using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using EstateManagementMvc;

namespace EsoftPortalMvc.Services.Common
{
    public class MenuManager
    {
        EsoftPortalEntities db = new EsoftPortalEntities();
        public bool SaveSystemMenus(List<NavigationMenu> systemMenus)
        {
            StringBuilder sb = new StringBuilder();
            bool result = false;
            foreach (var menu in systemMenus)
            {
                sb.AppendLine("MERGE tbl_NavigationMenus  AS TARGET using (SELECT " +
                   menu.MenuId.ToString().Format_Sql_String() + ",'" +
                    menu.MenuName.Format_Sql_String() + "','" +
                    menu.Action.Format_Sql_String() + "','" +
                    menu.Controller.Format_Sql_String() + "','" +
                    menu.Area.Format_Sql_String() + "','" +
                    menu.Icon.Format_Sql_String() + "','" +
                    menu.ModuleId.Format_Sql_String() + "'," +
                    menu.ParentId.ToString().Format_Sql_String() + ",'" +
                    ValueConverters.ConvertBoolToSqlString(menu.Report) + "' ) AS SOURCE " +
              " (MenuId,MenuName,Action,Controller,Area,Icon,ModuleId,ParentId,Report) on (TARGET.MenuId=source.MenuId " +
              " ) When Matched Then Update set MenuName = source.MenuName,Action=source.Action,Controller=Source.Controller,Area=Source.Area,Icon=source.Icon,ParentId=Source.Parentid,ModuleId=source.ModuleId,report=source.report WHEN NOT MATCHED THEN " +
              "insert (MenuId,MenuName,Action,Controller,Area,Icon,ParentId,ModuleId,Report)values(Source.MenuId,Source.MenuName,source.Action,Source.Controller,Source.Area,Source.Icon,Source.ParentId,source.ModuleId,source.Report);");


            }
            DbConnector.ExecuteSQL("DELETE tbl_NavigationMenus");
            DbConnector.ExecuteSQL("TRUNCATE TABLE tbl_NavigationMenus");

            DbConnector.ExecuteSQL(sb.ToString());

            CheckModuleNames();
            sb = null;
            return result;
        }

        private void CheckModuleNames()
        {
            List<NavigationMenu> modulenames = new List<NavigationMenu>();
            modulenames.Add(new NavigationMenu { MenuId = 500411, MenuName = "Create Account Closure Request" });
            modulenames.Add(new NavigationMenu { MenuId = 500412, MenuName = "Authorise Account Closure Request" });
            modulenames.Add(new NavigationMenu { MenuId = 500431, MenuName = "Create Member Rejoining Request" });
            modulenames.Add(new NavigationMenu { MenuId = 500432, MenuName = "Authorise Member Rejoining Request" });
            modulenames.Add(new NavigationMenu { MenuId = 600300, MenuName = "BOD Allowances" });

            StringBuilder sb = new StringBuilder();

            foreach (var menu in modulenames)
            {
                sb.AppendLine("MERGE tbl_ModuleNames  AS TARGET using (SELECT '" +
                   menu.MenuId.ToString().Format_Sql_String() + "','" +
                    menu.MenuName.Format_Sql_String() + "') AS SOURCE " +
              " (ModuleId,ModuleName) on (TARGET.ModuleId=source.ModuleId) " +
              "  When Matched Then Update set ModuleName = source.ModuleName WHEN NOT MATCHED THEN " +
              " insert (ModuleId,ModuleName)values(Source.ModuleId,Source.ModuleName);");
            }
            DbConnector.ExecuteSQL(sb.ToString());

            string insertMenus = "insert into tbl_ModuleNames(ModuleId,ModuleName) select b.ModuleId,(Select top 1 MenuName  from tbl_NavigationMenus c where c.ModuleId=b.ModuleId) as MenuName from tbl_NavigationMenus b " +
                        " where coalesce(b.ModuleId,'')<>'' and isnull((Select count(1) from tbl_ModuleNames a where a.ModuleId=b.ModuleId),0)=0 group by b.ModuleId";
            DbConnector.ExecuteSQL(insertMenus);

            // Module ids not in Menu
            sb.Clear();
            sb.AppendLine("insert into tbl_ModuleNames(ModuleId,ModuleName) select '4006A1','Loan Collaterals - Maintain' " +
                        " where isnull((Select count(1) from tbl_ModuleNames a where a.ModuleId='4006A1'),0)=0");

            DbConnector.ExecuteSQL(sb.ToString());

            modulenames = null;
        }

        public List<NavigationMenu> GetUserMenuItems(string accessRights)
        {
            List<NavigationMenu> userMenuIds = new List<NavigationMenu>();

            // Disable on Installation
            NavigationMenu navMenu = new NavigationMenu();
            navMenu.GenerateMenus();
            navMenu = null;

            try
            {
                DbDataReader reader = null;
                reader = DbConnector.GetSqlReader("SELECT MenuId,MenuName,Action,Controller,Area,Icon,ParentId,ModuleId,Report from tbl_NavigationMenus ");// where MenuId in(30000) or menuid like '3005%'");
                while (reader.Read())
                {
                    if (accessRights.Contains(reader["ModuleId"].ToString()))
                    {
                        string controller = reader["Controller"].ToString().ConvertNullToEmptyString().Trim();
                        string url = controller + @"/" + reader["Action"].ToString().ConvertNullToEmptyString().Trim();
                        string area = reader["Area"].ToString().ConvertNullToEmptyString().Trim();
                        bool report = ValueConverters.StringtoBool(reader["Report"].ToString());
                        if (!ValueConverters.IsStringEmpty(area))
                        {
                            url = area + @"/" + url;
                        }
                        userMenuIds.Add(new NavigationMenu
                        {
                            MenuId = ValueConverters.StringtoInteger(reader["MenuId"].ToString()),
                            MenuName = ValueConverters.ConvertNullToEmptyString(reader["MenuName"].ToString()),
                            Action = ValueConverters.ConvertNullToEmptyString(reader["Action"].ToString()),
                            Controller = controller,
                            Area = area,
                            Icon = ValueConverters.ConvertNullToEmptyString(reader["Icon"].ToString()),
                            ParentId = ValueConverters.StringtoInteger(reader["ParentId"].ToString()),
                            ModuleId = ValueConverters.ConvertNullToEmptyString(reader["ModuleId"].ToString()),
                            Url = url,
                            Report = report
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("GetUserMenuItems", ref ex);
            }

            return userMenuIds;
        }

        public List<AutoCompleteItem> FetchMenusCached(List<AutoCompleteItem> autoCompleteItem, string term)
        {
            try
            {
                const string systemMenus_cacheKey = "AllSystemMenus_cache";
                autoCompleteItem = CachingProvider.Get<List<AutoCompleteItem>>(systemMenus_cacheKey);
                if (autoCompleteItem == null)
                {
                    autoCompleteItem = new List<AutoCompleteItem>();
                    List<NavigationMenu> allMenuIds = new List<NavigationMenu>();
                    DbDataReader reader = DbConnector.GetSqlReader("SELECT TOP(10) MenuName,Action,Controller,Area from tbl_NavigationMenus where MenuName LIKE '%" + term + "%' AND coalesce(Action,'')<>'' ");
                    while (reader.Read())
                    {
                        allMenuIds.Add(new NavigationMenu
                        {
                            MenuName = reader["MenuName"].ToString(),
                            Action = reader["Action"].ToString(),
                            Controller = reader["Controller"].ToString(),
                            Area = reader["Area"].ToString(),
                        });
                    }

                    StringBuilder strBui = new StringBuilder();
                    foreach (var item in allMenuIds)
                    {
                        strBui.Append("/" + item.Area.Trim() + "/" + item.Controller.Trim() + "/" + item.Action.Trim());
                        autoCompleteItem.Add(new AutoCompleteItem
                        {
                            label = item.MenuName,
                            value = strBui.ToString()
                        });
                        strBui.Clear();
                    }
                    CachingProvider.Add(autoCompleteItem, systemMenus_cacheKey);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("FetchMenus", ref ex);
            }
            return autoCompleteItem;
        }

        public List<AutoCompleteItem> FetchMenus(List<AutoCompleteItem> autoCompleteItem, string searchTerm, string baseUrl)
        {

            autoCompleteItem = new List<AutoCompleteItem>();
            List<NavigationMenu> allMenuIds = new List<NavigationMenu>();
            searchTerm = searchTerm.ToUpper().Trim();

            if (UserSession.Current.UserMenuIds != null)
            {
                int count = UserSession.Current.UserMenuIds.Count;
                for (int i = 0; i < count; i++)
                {
                    if (UserSession.Current.UserMenuIds[i].MenuName.ToUpper().Contains(searchTerm) && !string.IsNullOrWhiteSpace(UserSession.Current.UserMenuIds[i].Action))
                    {
                        allMenuIds.Add(UserSession.Current.UserMenuIds[i]);
                    }
                    if (allMenuIds.Count > 15)
                        break;
                }
            }

            StringBuilder strBui = new StringBuilder();
            foreach (var item in allMenuIds)
            {
                strBui.Append(baseUrl + item.Area.Trim() + "/" + item.Controller.Trim() + "/" + item.Action.Trim());
                autoCompleteItem.Add(new AutoCompleteItem
                {
                    label = item.MenuName,
                    value = strBui.ToString()
                });
                strBui.Clear();
            }
            return autoCompleteItem;
        }

        public List<AutoCompleteItem> GetReportMenus(string menuArea)
        {

            List<AutoCompleteItem> autoCompleteItem = new List<AutoCompleteItem>();
            try
            {
                DbDataReader reader = DbConnector.GetSqlReader("SELECT MenuId, MenuName from tbl_NavigationMenus where Area  in('" + menuArea + "') AND Report = 'true' ");
                while (reader.Read())
                {
                    autoCompleteItem.Add(new AutoCompleteItem
                    {
                        label = reader["MenuName"].ToString(),
                        value = reader["MenuId"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                Utility.WriteErrorLog("BankingGetReportMenus", ref ex);
            }
            return autoCompleteItem;
        }

        public List<NavMenuViewModel> GetMenuRoute(string rptCode)
        {
            List<NavMenuViewModel> menuRoutes = new List<NavMenuViewModel>();
            DbDataReader reader = DbConnector.GetSqlReader("SELECT MenuId, MenuName,Action,Controller,Area from tbl_NavigationMenus where MenuId = ' " + rptCode + "'");
            while (reader.Read())
            {
                menuRoutes.Add(new NavMenuViewModel
                {
                    menuAction = reader["Action"].ToString(),
                    menuController = reader["Controller"].ToString(),
                    menuArea = reader["Area"].ToString()

                });
            }


            return menuRoutes;

        }

    }
}