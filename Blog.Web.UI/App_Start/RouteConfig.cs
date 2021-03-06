﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Web.UI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 name: "Post",
				 url: "Post/{url}",
				 defaults: new { controller = "Post", action = "Index", url = UrlParameter.Optional }
			);

			routes.MapRoute(
				 name: "Posts",
				 url: "Posts/{page}",
				 defaults: new { controller = "Home", action = "Index", page = UrlParameter.Optional }
			);

			routes.MapRoute(
				 name: "Default",
				 url: "{controller}/{action}/{id}",
				 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}