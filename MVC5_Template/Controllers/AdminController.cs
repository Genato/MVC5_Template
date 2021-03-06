﻿using Microsoft.AspNet.Identity;
using MVC5_Template.BusinessLogic;
using MVC5_Template.Models.IdentityModels;
using MVC5_Template.ViewModels.AdminViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using MVC5_Template.Localization;
using MVC5_Template.Models.MVC5_TemplateModels;
using MVC5_Template.BusinessLogic.Identity;

namespace MVC5_Template.Controllers
{
  [Authorize(Roles = "Admin")]
    public class AdminController : MVC5_TemplateBaseController
    {
        public AdminController(MVC5_TemplateIdentityRoleManager roleManager, LocaleLogic localeLogic, MVC5_TemplateIdentityUserManager userManager, UserSettingsLogic userSettingsLogic)
        {
            _UserSettingsLogic = userSettingsLogic;
            _UserManager = userManager;
            _LocaleLogic = localeLogic;
            _RoleManager = roleManager;
        }

        [HttpGet]
        public ActionResult AdminSettings()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel()
            {
                Localization = _LocaleLogic.GetLocalizations()
            };

            return View(registerViewModel);
        }

        /// <summary>
        /// POST: /Account/Register <para/>
        /// Action register new user to {schema}.{table} => {identity}.{Users} and UserSettings to {MVC5_Template}.{User_Settings}. <para/>
        /// It also sets localization for currently created and loged in user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel user)
        {
            user.Localization = _LocaleLogic.GetLocalizations();

            if (ModelState.IsValid == false)
                return View(user);

            MVC5_TemplateIdentityUser _user = new MVC5_TemplateIdentityUser { UserName = user.Email, Email = user.Email };
            var result = await _UserManager.CreateAsync(_user, user.Password);

            if (result.Succeeded == false)
            {
                _UserSettingsLogic.AddErrors(ModelState, result);
                return View(user);
            }

            //Create UserSettings and set localization for currently created user
            MVC5_TemplateIdentityUser MVC5_TemplateUser = _UserManager.Find(user.Email, user.Password);

            UserSettings userSettings = new UserSettings()
            {
                UserID = MVC5_TemplateUser.Id,
                LocalizationID = user.SelectedLocale
            };

            _UserSettingsLogic.CreateEntity(userSettings);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// This function/action
        /// </summary>
        /// <param name="createRoleViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(CreateRoleViewModel createRoleViewModel)
        {
            MVC5_TemplateIdentityRole role = new MVC5_TemplateIdentityRole(createRoleViewModel.RoleName);

            var result = await _RoleManager.CreateAsync(role);

            if (result.Succeeded == false)
            {
                _UserSettingsLogic.AddErrors(ModelState, result);
                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult ManageRoles(int pageNumber = 1, int pageSize = 5)
        {
            ManageRolesViewModel manageRolesViewModel = new ManageRolesViewModel()
            {
                PagedListOfRoles = _RoleManager.Roles.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize)
            };

            return View(manageRolesViewModel);
        }

        /// <summary>
        /// This action delete role.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteRole(string roleName)
        {
            bool result = await _RoleManager.DeleteRoleAsync(roleName);

            string resultMessage = result ? SuccessMsg.RoleDeletedSuccesfully : ErrorMsg.RoleDeletedError;

            return Json(resultMessage);
        }

        [HttpGet]
        public async Task<ActionResult> EditRole(string roleName)
        {
            MVC5_TemplateIdentityRole MVC5_TemplateIdentityRole = await _RoleManager.FindByNameAsync(roleName);

            EditRoleViewModel editRoleViewModel = new EditRoleViewModel()
            {
                RoleID = MVC5_TemplateIdentityRole.Id,
                RoleName = MVC5_TemplateIdentityRole.Name
            };

            return View(editRoleViewModel);
        }

        /// <summary>
        /// This action edit role name.
        /// </summary>
        /// <param name="MVC5_TemplateIdentityRole"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<JsonResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            bool result = await _RoleManager.EditRoleAsync(editRoleViewModel);

            string resultMessage = result ? SuccessMsg.RoleEditSuccesfully : ErrorMsg.RoleEditError;

            return Json(resultMessage);
        }

        [HttpGet]
        public ActionResult ManageUserRole(string roleName, string roleID)
        {
            List<MVC5_TemplateIdentityUser> listOfUsers = _UserManager.GetListOfUsersForRole(roleID);

            ManageUserRolesViewModel manageUserRolesViewModel = new ManageUserRolesViewModel()
            {
                RoleName = roleName,
                ListOfUser = listOfUsers,
                RoleID = roleID
            };

            return View(manageUserRolesViewModel);
        }

        /// <summary>
        /// This action adds user to role.
        /// </summary>
        /// <param name="manageUserRolesViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> AddUserToRole(ManageUserRolesViewModel manageUserRolesViewModel)
        {
            MVC5_TemplateIdentityUser MVC5_TemplateIdentityUser = await _UserManager.FindByNameAsync(manageUserRolesViewModel.UserName);

            string resultMessage;

            if (MVC5_TemplateIdentityUser == null)
            {
                resultMessage = ErrorMsg.UserDoesNotExists;

                return Json(resultMessage);
            }

            IdentityResult result = await _UserManager.AddToRoleAsync(MVC5_TemplateIdentityUser.Id, manageUserRolesViewModel.RoleName);

            resultMessage = result.Succeeded ? SuccessMsg.UserAddedToRoleSuccesfuly : ErrorMsg.UserAddedToRoleError;

            return Json(resultMessage);
        }

        /// <summary>
        /// This action removes user from role.
        /// </summary>
        /// <param name="manageUserRolesViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> RemoveUserFromRole(ManageUserRolesViewModel manageUserRolesViewModel)
        {
            MVC5_TemplateIdentityUser MVC5_TemplateIdentityUser = await _UserManager.FindByNameAsync(manageUserRolesViewModel.UserName);

            IdentityResult result = await _UserManager.RemoveFromRoleAsync(MVC5_TemplateIdentityUser.Id, manageUserRolesViewModel.RoleName);

            string resultMessage = result.Succeeded ? SuccessMsg.UserRemovedFromRoleSuccesfully : ErrorMsg.UserRemovedFromRoleError;

            return Json(resultMessage);
        }

        [HttpGet]
        public ActionResult ListOfUsersForRole(string roleID)
        {
            List<MVC5_TemplateIdentityUser> listOfUsers = _UserManager.GetListOfUsersForRole(roleID);

            return PartialView("~/Views/Admin/PartialViews/ListOfUsersForRole.cshtml", listOfUsers);
        }

        private UserSettingsLogic _UserSettingsLogic { get; set; }
        private MVC5_TemplateIdentityRoleManager _RoleManager { get; set; }
        private MVC5_TemplateIdentityUserManager _UserManager { get; set; }
        private LocaleLogic _LocaleLogic { get; set; }
    }
}