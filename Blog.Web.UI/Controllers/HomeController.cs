using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ReadModel.Repository;

namespace Blog.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			return View(Repositories.PublishedPosts.MostRecentPosts(1, 5, false));
		}
	}
}
