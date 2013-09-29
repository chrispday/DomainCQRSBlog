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
			if (Request.IsAuthenticated)
			{
				return Redirect("/");
			}

			return View();
		}

		[HttpPost]
		public ActionResult Index(string username, string password, string returnUrl)
		{
			var sessionId = Guid.NewGuid();
			try
			{
				DomainCQRSConfig.MessageReceiver.Receive(new Blog.Domain.Commands.Login() { Username = username, Password = password, SessionId = sessionId });
			}
			catch (Blog.Domain.Errors.WrongUsernameOrPasswordError)
			{
				ModelState.AddModelError("", "Wrong username or password.");
				return View();
			}
			FormsAuthentication.SetAuthCookie(sessionId.ToString(), false);
			returnUrl = returnUrl ?? "/";
			return Redirect(returnUrl);
		}

		[Authorize, HttpGet]
		public ActionResult Edit()
		{
			return View(Repositories.Users.Get(Repositories.Sessions.Get(new Guid(User.Identity.Name)).UserId));
		}

		[Authorize, HttpPost]
		public ActionResult Edit(string currentPassword, string newPassword, string confirmPassword, Guid? userId, string returnUrl)
		{
			if (newPassword != confirmPassword)
			{
				ModelState.AddModelError("", "Passwords must match.");
				return Edit();
			}

			try
			{
				DomainCQRSConfig.MessageReceiver.Receive(new Blog.Domain.Commands.ChangePassword() { Id = userId.Value, NewPassword = newPassword, OldPassword = currentPassword });
			}
			catch (Blog.Domain.Errors.WrongUsernameOrPasswordError)
			{
				ModelState.AddModelError("", "Wrong username or password.");
				return Edit();
			}

			returnUrl = returnUrl ?? "/";
			return Redirect(returnUrl);
		}

		public ActionResult Signout(string returnUrl)
		{
			FormsAuthentication.SignOut();

			returnUrl = returnUrl ?? "/";
			return Redirect(returnUrl);
		}
	}
}
