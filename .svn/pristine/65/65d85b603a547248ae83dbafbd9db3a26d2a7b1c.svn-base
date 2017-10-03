using EstateManagementMvc.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstateManagementMvc.Services.UserAdministration
{
    public static class CheckUserAccessRights
    {
        public static bool CheckUserAccess(string moduleId)
        {
            if (UserSession.Current == null || UserSession.Current.userDetails == null || ValueConverters.IsStringEmpty(UserSession.Current.userDetails.AccessRights))
            {
                return false;
            }
            return UserSession.Current.userDetails.AccessRights.Contains(moduleId);
        }
    }
}