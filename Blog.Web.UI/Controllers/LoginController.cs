using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
			var sessionId = Guid.NewGuid();
			try
			{
				YeastConfig.MessageReceiver.Receive(new Blog.Domain.Commands.Login() { Username = username, Password = password, SessionId = sessionId });
			}
			catch (Blog.Domain.Errors.WrongUsernameOrPasswordError)
			{
				ModelState.AddModelError("", "Wrong username or password.");
				return View();
			}
			FormsAuthentication.SetAuthCookie(sessionId.ToString(), false);
			referrerUrl = referrerUrl ?? "/";
			return Redirect(referrerUrl);
		}

		public ActionResult Logout(string referrerUrl)
		{
			FormsAuthentication.SignOut();

			if (!Request.IsAjaxRequest())
			{
				return new HttpStatusCodeResult((int)HttpStatusCode.OK);
			}

			referrerUrl = referrerUrl ?? "/";
			return Redirect(referrerUrl);
		}
	}
}
