using Microsoft.AspNet.Identity;
using MVC5_Template.Localization;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MVC5_Template.BusinessLogic.Identity.Extensions
{
    /// <summary>
    /// Custom user validator. <para/>
    /// NOTE: This class inherited and override "UserValidator<T> ValidateAsync(T user)", because standard Identity validation does not support Globalization/Localization
    /// </summary>
    public class CustomUserValidator : UserValidator<MVC5_TemplateIdentityUser>
    {
        public CustomUserValidator(UserManager<MVC5_TemplateIdentityUser, string> manager) : base(manager)
        {
            _Errors = new List<string>();
            _Manager = manager;
        }

        public override Task<IdentityResult> ValidateAsync(MVC5_TemplateIdentityUser user)
        {
            if(user.EmailConfirmed == false)
                ValidateEmail(user);
            
            IdentityResult result = _Errors.Count > 0 ? new IdentityResult(_Errors.ToArray()) : IdentityResult.Success;

            return Task.FromResult(result);
        }

        /// <summary>
        /// Method that adds localization for Identity user validation.
        /// </summary>
        /// <param name="user"></param>
        private void ValidateEmail(MVC5_TemplateIdentityUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                _Errors.Add(ErrorMsg.EmailMustBeSpecified);
                return;
            }

            Task<MVC5_TemplateIdentityUser> _user =  _Manager.FindByEmailAsync(user.Email);

            if (_user.Result != null && _user.Result.Email == user.Email)
                _Errors.Add(ErrorMsg.EmailAllreadyExists);
        }

        private List<string> _Errors { get; set; }
        public UserManager<MVC5_TemplateIdentityUser, string> _Manager { get; set; }
    }
}