using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC5_Template.BusinessLogic;
using MVC5_Template.Localization;
using MVC5_Template.Models.IdentityModels;
using MVC5_Template.ViewModels.AccountViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC5_Template.Models.MVC5_TemplateModels;
using MVC5_Template.BusinessLogic.Identity;

namespace MVC5_Template.Controllers
{
  public class AccountController : MVC5_TemplateBaseController
    {
        public AccountController(MVC5_TemplateIdentitySignInManager signInManager, MVC5_TemplateIdentityUserManager userManager, IAuthenticationManager authenticationManager, UserSettingsLogic userSettingsLogic, LocaleLogic localeLogic)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
            _AuthenticationManager = authenticationManager;
            _UserSettingsLogic = userSettingsLogic;
            _LocaleLogic = localeLogic;
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// POST: /Account/Login <para/>
        /// This action login user and sets localization for it.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid == false)
                return View(model);
            
            var result = await _SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            if(result == SignInStatus.Failure)
            {
                ModelState.AddModelError("", ErrorMsg.InvaliLoginAttempt);
                return View(model);
            }

            // If user is successfully logged in set localization for it
            MVC5_TemplateIdentityUser MVC5_TemplateUser = _UserManager.Find(model.Email, model.Password);
            _LocaleLogic.SetLocalizationForCurrentUser(MVC5_TemplateUser.Id);

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Get Identity profile settings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IdentityProfileSettings()
        {
            MVC5_TemplateIdentityUser MVC5_TemplateUser = _UserManager.FindById(User.Identity.GetUserId());

            IdentityProfileViewModel profileViewModel = new IdentityProfileViewModel()
            {
                CurrentEmail = MVC5_TemplateUser.Email,
            };

            return View(profileViewModel);
        }

    /// <summary>
    /// Save MVC5_TemplateIdenityUser changes to {identity}.{Users} table.
    /// </summary>
    /// <param name="profileViewModel"></param>
    /// <returns></returns>
    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IdentityProfileSettings(IdentityProfileViewModel identityProfileViewModel)
        {
            if (ModelState.IsValid == false)
                return View(identityProfileViewModel);

            IdentityResult identityResult = await _UserManager.UpdateUser(identityProfileViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);
            
            return RedirectToAction("IdentityProfileSettings");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if(ModelState.IsValid == false)
                return View();

            IdentityResult identityResult = await _UserManager.UpdateUserPassword(changePasswordViewModel);

            if (identityResult.Succeeded == false)
                _UserSettingsLogic.AddErrors(ModelState, identityResult);

            return View();
        }

        /// <summary>
        /// Get user application settings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserAppSettings()
        {
            MVC5_TemplateIdentityUser MVC5_TemplateUser = _UserManager.FindById(User.Identity.GetUserId());

            UserAppSettingsViewModel userAppSettings = new UserAppSettingsViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations(),
                SelectedLocale = _UserSettingsLogic.GetByUserID(MVC5_TemplateUser.Id).LocalizationID
            };

            return View(userAppSettings);
        }

        /// <summary>
        /// Save UserSettings changes to {MVC5_Template}.{User_Settings} table
        /// </summary>
        /// <param name="userSettings"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAppSettings(UserAppSettingsViewModel userAppSettingsViewModel)
        {
            if (ModelState.IsValid == false)
            {
                userAppSettingsViewModel.Localization = _LocaleLogic.GetLocalizations();
                return View(userAppSettingsViewModel);
            }

            UserSettings userSettings = new UserSettings()
            {
                LocalizationID = userAppSettingsViewModel.SelectedLocale,
                UserID = User.Identity.GetUserId()
            };

            //Save User app settings and set localization for current user
            _UserSettingsLogic.SaveUserSettings(userSettings);
            _LocaleLogic.SetLocalizationForCurrentUser(User.Identity.GetUserId());

            return RedirectToAction("UserAppSettings");
        }

        ///Private members section
        private MVC5_TemplateIdentitySignInManager _SignInManager { get; set; }
        private MVC5_TemplateIdentityUserManager _UserManager { get; set; }
        private IAuthenticationManager _AuthenticationManager { get; set; }
        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }
    }
}