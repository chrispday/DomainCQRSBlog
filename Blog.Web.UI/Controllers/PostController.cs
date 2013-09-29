using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ReadModel.Repository;

namespace Blog.Web.UI.Controllers
{
	public class PostController : Controller
	{
		//
		// GET: /Post/

		[HttpGet]
		public ActionResult Index(string url)
		{
			var post = Repositories.PublishedPosts.GetByUrl(url);

			ViewBag.Comments = new Dictionary<Guid, IEnumerable<Blog.ReadModel.Data.Comment>>();
			ViewBag.Comments[post.Id] = (0 != post.TotalComments) ? Repositories.Comments.GetForPost(post.Id) : Enumerable.Empty<Blog.ReadModel.Data.Comment>();

			return View(post);
		}

		[HttpGet]
		public JsonResult Comments(Guid? id)
		{
			return Json(Repositories.Comments.GetForPost(id.Value), JsonRequestBehavior.AllowGet);
		}
	}
}
