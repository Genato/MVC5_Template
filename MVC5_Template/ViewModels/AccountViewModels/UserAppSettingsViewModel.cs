using MVC5_Template.Localization;
using MVC5_Template.Models.MVC5_TemplateModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC5_Template.ViewModels.AccountViewModels
{
  public class UserAppSettingsViewModel
    {
        [Display(Name = nameof(Labels.SelectLanguage), ResourceType = typeof(Labels))]
        public List<Locale> Localization { get; set; }

        public int SelectedLocale { get; set; }
    }
}