using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Blog.ReadModel.Repository;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Blog.Web.UI.Controllers
{
	[Authorize]
	public class DraftsController : Controller
	{
		//
		// GET: /Drafts/

		public ActionResult Index()
		{
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
		public ActionResult Edit(Blog.ReadModel.Data.DraftPost draftPost)
		{
			var id = draftPost.Id;
			if (Guid.Empty == id)
			{
				id = Guid.NewGuid();
				YeastConfig.MessageReceiver.Receive(new Blog.Domain.Commands.CreatePost()
				{
					Id = id,
					Title = draftPost.Title,
					WhenCreated = DateTime.Now,
					SessionId = new Guid(User.Identity.Name)
				});
			}

			YeastConfig.MessageReceiver.Receive(new Blog.Domain.Commands.EditPost()
			{
				Id = id,
				Title = draftPost.Title,
				Content = draftPost.Content,
				WhenEdited = DateTime.Now,
				SessionId = new Guid(User.Identity.Name)
			});

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Index(string id)
		{
			var postId = new Guid(id);
			YeastConfig.MessageReceiver.Receive(new Blog.Domain.Commands.PublishPost() { Id = postId, WhenPublished = DateTime.Now, SessionId = new Guid(User.Identity.Name) });
			return Redirect("/");
		}

		[HttpPost]
		public void FileUpload(HttpPostedFileBase file)
		{
			if (null != file
				&& file.ContentLength > 0)
			{
				var blobClient = Azure.StorageAccount.CreateCloudBlobClient();
				var container = blobClient.GetContainerReference("public");
				container.CreateIfNotExists();
				var blob = container.GetBlockBlobReference("/public/" + Path.GetFileName(file.FileName));
				blob.UploadFromStream(file.InputStream);
				blob.Properties.CacheControl = "public,max-age=31536000";
				blob.SetProperties();
			}
		}
	}
}
