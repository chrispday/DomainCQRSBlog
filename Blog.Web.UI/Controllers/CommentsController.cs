﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.UI.Controllers
{
	public class CommentsController : Controller
	{
		[HttpPost]
		public JsonResult Add(CommentWithRecaptcha comment)
		{
			var validator = new Recaptcha.RecaptchaValidator();
			validator.Challenge = comment.recaptcha_challenge_field;
			validator.Response = comment.recaptcha_response_field;
			validator.PrivateKey = "6LdJ_-cSAAAAAA2WUXbOjLdFdU267GEK006Dm84J";
			validator.RemoteIP = Request.UserHostAddress;
			var response = validator.Validate();
			if (!response.IsValid)
			{
				return Json(new { IsValid = false, RecaptchaResponse = response });
			}

			try
			{
				DomainCQRSConfig.MessageReceiver.Receive(new Blog.Domain.Commands.AddCommentToPost()
				{
					Id = comment.PostId,
					CommentId = comment.Id,
					Comment = comment.CommentText,
					EmailHash = comment.EmailHash,
					Name = comment.Name,
					Homepage = comment.Homepage,
					WhenCommented = DateTime.Now
				});

				return Json(new { IsValid = true });
			}
			catch (Exception ex)
			{
				return Json(new { IsValid = false, Message = ex.Message });
			}
		}
	}

	 public class CommentWithRecaptcha : Blog.ReadModel.Data.Comment
	 {
		 public string recaptcha_challenge_field { get; set; }
		 public string recaptcha_response_field { get; set; }
	 }
}
