using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.UI
{
	public class AuthorisedNotCachedAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.HttpContext.Response.Cache.AddValidationCallback(DontCacheAuthorised, null);
			base.OnActionExecuting(filterContext);
		}

		private void DontCacheAuthorised(HttpContext context, object data, ref HttpValidationStatus validationStatus)
		{
			if (context.Request.IsAuthenticated)
			{
				validationStatus = HttpValidationStatus.IgnoreThisRequest;
				context.Response.Cache.SetNoServerCaching();
				context.Response.Cache.SetNoStore();
			}
		}
	}
}