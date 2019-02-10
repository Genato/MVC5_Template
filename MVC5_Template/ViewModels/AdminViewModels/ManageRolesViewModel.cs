using MVC5_Template.Models.IdentityModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_Template.ViewModels.AdminViewModels
{
    public class ManageRolesViewModel
    {
        public IPagedList<MVC5_TemplateIdentityRole> PagedListOfRoles { get; set; }
    }
}