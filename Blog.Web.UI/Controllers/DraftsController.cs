using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ReadModel.Repository;

namespace Blog.Web.UI.Controllers
{
	public class DraftsController : Controller
	{
		//
		// GET: /Drafts/

		public ActionResult Index()
		{
			if (Request.QueryString.AllKeys.Contains("SessionId"))
			{
				Response.Cookies.Add(new HttpCookie("SessionId", Request.QueryString["SessionId"]));
			}
			return View(Repositories.DraftPosts.Get());
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{
			var draftPost = new Blog.ReadModel.Data.DraftPost() { Title = "", Content = "" };
			if (null != id)
			{
				draftPost = Repositories.DraftPosts.Get(new Guid(id));
			}
			return View(draftPost);
		}

		[HttpPost]
		[ValidateInput(false)]
		public void Edit(Blog.ReadModel.Data.DraftPost draftPost)
		{
			var id = draftPost.Id;
			if (Guid.Empty == id)
			{
				id = Guid.NewGuid();
				Config.MessageReceiver.Receive(new Blog.Domain.Commands.CreatePost()
				{
					Id = id,
					Title = draftPost.Title,
					WhenCreated = DateTime.Now,
					SessionId = Config.SessionId(Request.Cookies)
				});
			}

			Config.MessageReceiver.Receive(new Blog.Domain.Commands.EditPost()
			{
				Id = id,
				Title = draftPost.Title,
				Content = draftPost.Content,
				WhenEdited = DateTime.Now,
				SessionId = Config.SessionId(Request.Cookies)
			});

			RedirectToAction("Index");
		}

		[HttpPost]
		public void Index(string id)
		{
			var postId = new Guid(id);
			var sessionId = Config.SessionId(Request.Cookies);

			Config.MessageReceiver.Receive(new Blog.Domain.Commands.PublishPost() { Id = postId, WhenPublished = DateTime.Now, SessionId = sessionId });
			Redirect("/");
		}
	}
}
