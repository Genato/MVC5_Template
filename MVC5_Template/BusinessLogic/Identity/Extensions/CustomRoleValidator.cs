using Microsoft.AspNet.Identity;
using MVC5_Template.Localization;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC5_Template.BusinessLogic.Identity.Extensions
{
    public class CustomRoleValidator : RoleValidator<MVC5_TemplateIdentityRole>
    {
        public CustomRoleValidator(RoleManager<MVC5_TemplateIdentityRole, string> roleManager) : base(roleManager)
        {
            _RoleManager = roleManager;
            _Errors = new List<string>();
        }

        public override Task<IdentityResult> ValidateAsync(MVC5_TemplateIdentityRole role)
        {
            ValidateRoleName(role);

            IdentityResult result = _Errors.Count > 0 ? new IdentityResult(_Errors.ToArray()) : IdentityResult.Success;

            return Task.FromResult(result);
        }

        private void ValidateRoleName(MVC5_TemplateIdentityRole role)
        {
            if (string.IsNullOrWhiteSpace(role.Name))
            {
                _Errors.Add(ErrorMsg.RoleNameMustBeSpecified);

                return;
            }

            Task<MVC5_TemplateIdentityRole> _role = _RoleManager.FindByNameAsync(role.Name);

            if (_role.Result != null && _role.Result.Name == role.Name)
                _Errors.Add(ErrorMsg.RoleAllreadyExists);

        }

        private List<string> _Errors { get; set; }
        public RoleManager<MVC5_TemplateIdentityRole, string> _RoleManager { get; set; }
    }
}