using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MVC5_Template.BusinessLogic.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class MVC5_TemplateIdentitySignInManager : SignInManager<MVC5_TemplateIdentityUser, string>
    {
        public MVC5_TemplateIdentitySignInManager(MVC5_TemplateIdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(MVC5_TemplateIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((MVC5_TemplateIdentityUserManager)UserManager);
        }

        public static MVC5_TemplateIdentitySignInManager Create(IdentityFactoryOptions<MVC5_TemplateIdentitySignInManager> options, IOwinContext context)
        {
            return new MVC5_TemplateIdentitySignInManager(context.GetUserManager<MVC5_TemplateIdentityUserManager>(), context.Authentication);
        }
    }
}