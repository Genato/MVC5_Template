using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MVC5_Template.DbContexts;
using MVC5_Template.Models.IdentityModels;
using MVC5_Template.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MVC5_Template.BusinessLogic.Identity.Extensions;

namespace MVC5_Template.BusinessLogic.Identity
{
    public class MVC5_TemplateIdentityRoleManager : RoleManager<MVC5_TemplateIdentityRole>
    {
        public MVC5_TemplateIdentityRoleManager(IRoleStore<MVC5_TemplateIdentityRole, string> roleStore) : base(roleStore) { }

        public async Task<bool> DeleteRoleAsync(string roleName)
        {
            MVC5_TemplateIdentityRole role = await this.FindByNameAsync(roleName);

            return DeleteAsync(role).Result.Succeeded ? true : false;
        }

        public async Task<bool> EditRoleAsync(EditRoleViewModel editRoleViewModel)
        {
            MVC5_TemplateIdentityRole oldRole = await this.FindByIdAsync(editRoleViewModel.RoleID);

            oldRole.Name = editRoleViewModel.RoleName;

            return UpdateAsync(oldRole).Result.Succeeded ? true : false;
        }

        public static MVC5_TemplateIdentityRoleManager Create(IdentityFactoryOptions<MVC5_TemplateIdentityRoleManager> options, IOwinContext context)
        {
            MVC5_TemplateIdentityRoleManager MVC5_TemplateIdentityRoleManager = new MVC5_TemplateIdentityRoleManager(new RoleStore<MVC5_TemplateIdentityRole>(context.Get<IdentityUserDbContext>()));

            MVC5_TemplateIdentityRoleManager.RoleValidator = new CustomRoleValidator(MVC5_TemplateIdentityRoleManager);

            return MVC5_TemplateIdentityRoleManager;
        }
    }
}