using EsoftPortalMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Common
{
    public class UserSession
    {

        // private constructor
        private UserSession()
        {
            //CTOR
        }

        // Gets the current session.
        public static UserSession Current
        {
            get
            {
                UserSession session =
                  (UserSession)HttpContext.Current.Session["__EsoftWebUser__"];
                if (session == null)
                {
                    session = new UserSession();
                    HttpContext.Current.Session["__EsoftWebUser__"] = session;
                }
                return session;
            }
        }

        public UserDetailsView userDetails { get; set; }
        public string MachineName { get; set; }
        public List<NavigationMenu> UserMenuIds { get; set; }
        public string UserImage { get; set; }
        public string Teller_Footer_Text { get; set; }
    }
}