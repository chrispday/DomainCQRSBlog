﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Blog.ReadModel.Repository;

namespace Blog.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		private readonly int PageSize = 5;

		[AuthorisedNotCached]
		[OutputCache(Duration = 300, Location = OutputCacheLocation.Server)]
		public ActionResult Index(int? page)
		{
			if (!page.HasValue)
			{
				page = 1;
			}

			var posts = Repositories.PublishedPosts.GetPagedPostsByMostRecentP(page.Value, PageSize, true);
			ViewBag.MorePosts = (5 < posts.Count());
			ViewBag.PageNumber = page.Value;

			ViewBag.Comments = new Dictionary<Guid, IEnumerable<Blog.ReadModel.Data.Comment>>();
			foreach (var post in posts.Take(5))
			{
				ViewBag.Comments[post.Id] = (0 != post.TotalComments) ? Repositories.Comments.GetForPost(post.Id) : Enumerable.Empty<Blog.ReadModel.Data.Comment>();
			}

			return View(posts.Take(5));
		}
	}
}
