using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Blog.ReadModel.Repository;
using DevTrends.MvcDonutCaching;

namespace Blog.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		[AuthorisedNotCached]
		[OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
		public ActionResult Index()
		{
			return View(Repositories.PublishedPosts.MostRecentPosts(1, 5, false));
		}
	}
}
