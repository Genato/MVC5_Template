﻿using MVC5_Template.Localization.Extensions;
using System.Web;
using System.Web.Mvc;

namespace MVC5_Template
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());

            //Custom filters
        }
    }
}
