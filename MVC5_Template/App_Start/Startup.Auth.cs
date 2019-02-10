using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using MVC5_Template.Models;
using MVC5_Template.DbContexts;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5_Template.Models.IdentityModels;
using MVC5_Template.BusinessLogic.Identity;

namespace MVC5_Template
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(IdentityUserDbContext.Create);
            app.CreatePerOwinContext<MVC5_TemplateIdentityUserManager>(MVC5_TemplateIdentityUserManager.Create);
            app.CreatePerOwinContext<MVC5_TemplateIdentitySignInManager>(MVC5_TemplateIdentitySignInManager.Create);
            app.CreatePerOwinContext<MVC5_TemplateIdentityRoleManager>(MVC5_TemplateIdentityRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/{lang}/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<MVC5_TemplateIdentityUserManager, MVC5_TemplateIdentityUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}