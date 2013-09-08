using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.ReadModel.Repository;

namespace Blog.Web.UI.Controllers
{
	public class LoginController : Controller
	{
		//
		// GET: /Login/

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public void Login(string username, string password, string referrerUrl)
		{
			Config.MessageReceiver.Receive(new Blog.Domain.Commands.Login() { Username = username, Password = password });
			var _referrerUrl = new UriBuilder(referrerUrl);
			var query = HttpUtility.ParseQueryString(_referrerUrl.Query ?? string.Empty);
			query["SessionId"] = Repositories.Sessions.Get(Repositories.Users.Get(username).Id).Id.ToString();
			_referrerUrl.Query = query.ToString();
			Redirect(_referrerUrl.ToString());
		}
	}
}
