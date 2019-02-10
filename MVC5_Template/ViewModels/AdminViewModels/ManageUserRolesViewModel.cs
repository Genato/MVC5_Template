using Microsoft.AspNet.Identity.EntityFramework;
using MVC5_Template.Localization;
using MVC5_Template.Models;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5_Template.ViewModels.AdminViewModels
{
    public class ManageUserRolesViewModel
    {
        [Display(Name = nameof(Labels.UserName), ResourceType = typeof(Labels))]
        public string UserName { get; set; }
        [Display(Name = nameof(Labels.RoleName), ResourceType = typeof(Labels))]
        public string RoleName { get; set; }

        public string RoleID { get; set; }

        public List<MVC5_TemplateIdentityUser> ListOfUser { get; set; }
    }
}