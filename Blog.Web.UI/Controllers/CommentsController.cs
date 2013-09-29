using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.UI.Controllers
{
    public class CommentsController : Controller
    {
		 [HttpPost]
		 public void Add(CommentWithRecaptcha comment)
		 {
			 var validator = new Recaptcha.RecaptchaValidator();
			 validator.Challenge = comment.recaptcha_challenge_field;
			 validator.Response = comment.recaptcha_response_field;
			 validator.PrivateKey = "6LdJ_-cSAAAAAA2WUXbOjLdFdU267GEK006Dm84J";
			 validator.RemoteIP = Request.UserHostAddress;
			 var response = validator.Validate();
			 if (!response.IsValid)
			 {
				 throw new Exception(response.ErrorMessage);
			 }

			 DomainCQRSConfig.MessageReceiver.Receive(new Blog.Domain.Commands.AddCommentToPost()
			 {
				 Id = comment.PostId,
				 CommentId = comment.Id,
				 Comment = comment.CommentText,
				 Email = comment.Email,
				 Name = comment.Name,
				 ShowEmail = comment.ShowEmail,
				 WhenCommented = DateTime.Now
			 });
		 }
	 }

	 public class CommentWithRecaptcha : Blog.ReadModel.Data.Comment
	 {
		 public string recaptcha_challenge_field { get; set; }
		 public string recaptcha_response_field { get; set; }
	 }
}
