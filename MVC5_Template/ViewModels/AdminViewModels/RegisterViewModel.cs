using MVC5_Template.Localization;
using MVC5_Template.Models.MVC5_TemplateModels;
using MVC5_Template.Models.IdentityModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5_Template.ViewModels.AdminViewModels
{
  public class RegisterViewModel
    {
        [Display(Name = nameof(Labels.Email), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.EmailMustBeSpecified), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.Password), ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(ErrorMsg.MinPasswordLength), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.PasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = nameof(Labels.ConfirmPassword), ResourceType = typeof(Labels))]
        [Compare("Password", ErrorMessageResourceName = nameof(ErrorMsg.PasswordsDontMatch), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.ConfirmPasswordIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string ConfirmPassword { get; set; }

        [Display(Name = nameof(Labels.SelectLanguage), ResourceType = typeof(Labels))]
        public List<Locale> Localization { get; set; }

        [Display(Name = nameof(Labels.IdentityRoles), ResourceType = typeof(Labels))]
        public List<MVC5_TemplateIdentityRole> Roles { get; set; }

        public int SelectedLocale { get; set; }
        public int SelectedRole { get; set; }
    }
}