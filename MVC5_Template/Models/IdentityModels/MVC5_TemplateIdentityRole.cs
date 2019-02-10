using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5_Template.Models.IdentityModels
{
    public class MVC5_TemplateIdentityRole : IdentityRole
    {
        public MVC5_TemplateIdentityRole() { }

        public MVC5_TemplateIdentityRole(string roleName) : base(roleName) { }
    }
}