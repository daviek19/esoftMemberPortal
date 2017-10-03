using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using EstateManagementMvc.Models;

namespace EstateManagementMvc
{
    public static class IdentityConfig
    {
        public static void RegisterIdentities()
        {
            // Ensures the default demo user is available to login with
            UserManager.Seed();
        }
    }
}
