using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace MVC5_Template.BusinessLogic
{
  public abstract class MVC5_TemplateLogic
    {

        // Public methods //

        /// <summary>
        /// Add errors to modelstate from IdentityResults. ModelState can be passed to ERROR view.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="result"></param>
        public void AddErrors(ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("IdentityErrors", error);
            }
        }

        /// <summary>
        /// Add errors to modelstate from modelStatw. ModelState can be passed to ERROR view.
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="result"></param>
        public void AddErrors(ModelStateDictionary modelState)
        {
            foreach (var error in modelState)
            {
                foreach (var item in error.Value.Errors)
                {
                    modelState.AddModelError("ModelError", item.ErrorMessage);
                }
            }
        }

        // Abstract methods //

        public abstract int CreateEntity<T>(T entity) where T : class;

        public abstract int SaveEntity<T>(T entity) where T : class;
    }
}