using MVC5_Template.Localization;
using MVC5_Template.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5_Template.ViewModels.AdminViewModels
{
    public class CreateRoleViewModel
    {
        [Display(Name = nameof(Labels.RoleName), ResourceType = typeof(Labels))]
        public string RoleName { get; set; }
    }
}