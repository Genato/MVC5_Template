using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MVC5_Template.Models;
using MVC5_Template.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5_Template.Models.IdentityModels;
using MVC5_Template.ViewModels.AccountViewModels;
using System.Threading.Tasks;
using MVC5_Template.BusinessLogic.Identity.Extensions;

namespace MVC5_Template.BusinessLogic.Identity
{
    /// <summary>
    /// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    /// </summary>
    public class MVC5_TemplateIdentityUserManager : UserManager<MVC5_TemplateIdentityUser>
    {
        public MVC5_TemplateIdentityUserManager(IUserStore<MVC5_TemplateIdentityUser> store) : base(store) { }

        public List<MVC5_TemplateIdentityUser> GetListOfUsersForRole(string roleID)
        {
            var _userRoles = (from userRole in this.Users
                              where userRole.Roles.Any(f => f.RoleId == roleID)
                              select userRole).ToList();

            return _userRoles;
        }

        /// <summary>
        /// Method updates all user properties (It doesn't update password !)
        /// </summary>
        /// <param name="profileViewModel"></param>
        public async Task<IdentityResult> UpdateUser(IdentityProfileViewModel profileViewModel)
        {
            MVC5_TemplateIdentityUser MVC5_TemplateIdentityUser = this.FindById(HttpContext.Current.User.Identity.GetUserId());
            MVC5_TemplateIdentityUser.Email = profileViewModel.NewEmail == null ? profileViewModel.CurrentEmail : profileViewModel.NewEmail;
            MVC5_TemplateIdentityUser.UserName = profileViewModel.NewEmail == null ? profileViewModel.CurrentEmail : profileViewModel.NewEmail;

            IdentityResult result = await this.UpdateAsync(MVC5_TemplateIdentityUser);

            return result;
        }

        /// <summary>
        /// Method only updates user password
        /// </summary>
        /// <param name="changePasswordViewModel"></param>
        public async Task<IdentityResult> UpdateUserPassword(ChangePasswordViewModel changePasswordViewModel)
        {            
            return await this.ChangePasswordAsync(HttpContext.Current.User.Identity.GetUserId(), changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
        }

        /// <summary>
        /// We override this function because we implemeted custom validator "CustomUserValidator" and when we try to add user to role it validate email and says that user email already exists.
        /// So we need to intercept and validate email before it goes to custom validator.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task<IdentityResult> AddToRoleAsync(string userId, string role)
        {
            string emailToken = this.GenerateEmailConfirmationToken(userId);

            this.ConfirmEmail(userId, emailToken);

            return base.AddToRoleAsync(userId, role);
        }

        public static MVC5_TemplateIdentityUserManager Create(IdentityFactoryOptions<MVC5_TemplateIdentityUserManager> options, IOwinContext context)
        {
            var manager = new MVC5_TemplateIdentityUserManager(new UserStore<MVC5_TemplateIdentityUser>(context.Get<IdentityUserDbContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new CustomUserValidator(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            
            // Configure validation logic for passwords
            manager.PasswordValidator = new CustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = false
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<MVC5_TemplateIdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}