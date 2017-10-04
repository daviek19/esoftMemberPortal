using EsoftPortalMvc.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.UserAdministration
{
    public static class CheckUserAccessRights
    {
        public static bool CheckUserAccess(string moduleId)
        {

            return false;// UserSession.Current.userDetails.AccessRights.Contains(moduleId);
        }
    }
}