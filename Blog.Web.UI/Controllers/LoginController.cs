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
		public ActionResult Index(string username, string password, string referrerUrl)
		{
			YeastConfig.MessageReceiver.Receive(new Blog.Domain.Commands.Login() { Username = username, Password = password });
			referrerUrl = referrerUrl ?? "/";
			referrerUrl += (referrerUrl.Contains("?") ? "&" : "?") + "SessionId=" + Repositories.Sessions.GetByUser(Repositories.Users.Get(username).Id).Id.ToString();
			return Redirect(referrerUrl);
		}
	}
}
