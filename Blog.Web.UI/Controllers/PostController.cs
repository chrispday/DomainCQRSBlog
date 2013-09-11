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

        public ActionResult Index(string url)
        {
			  return View(Repositories.PublishedPosts.GetByUrl(url));
		  }

    }
}
