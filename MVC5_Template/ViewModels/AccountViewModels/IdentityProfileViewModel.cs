using MVC5_Template.Localization;
using System.ComponentModel.DataAnnotations;

namespace MVC5_Template.ViewModels.AccountViewModels
{
  public class IdentityProfileViewModel
    {
        [Display(Name = nameof(Labels.CurrentEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.CurrentEmailIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string CurrentEmail { get; set; }

        [Display(Name = nameof(Labels.NewEmail), ResourceType = typeof(Labels))]
        [EmailAddress(ErrorMessageResourceName = nameof(ErrorMsg.EmailInvalidFormat), ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessageResourceName = nameof(ErrorMsg.NewEmailIsRequired), ErrorMessageResourceType = typeof(ErrorMsg))]
        public string NewEmail { get; set; }
    }
}