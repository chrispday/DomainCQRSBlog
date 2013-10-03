using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Repository;
using Blog.Domain.Errors;
using System.Security.Cryptography;
using System.Text;

[TestClass]
public class CommentOnPost_
{
	[TestMethod]
	public void CommentOnPost()
	{
		new Story("Comment on Post")
			 .InOrderTo("provide feedback OR ask a question OR answer a question.")
			 .AsA("Commenter")
			 .IWant("to comment on Posts.")

						.WithScenario("Comment on Post")
							 .Given(APost)
							 .When(ACommentIsAdded)
							 .Then(TheCommentAppearsWithThePost)
								  .And(ItHasTheCommentersName)
								  .And(ItHasTheCommentersEmailHash)
								  .And(ItHasTheCommentersHomepage)
								  .And(ItHasTheDatetime)

						.WithScenario("Empty Commenter Name")
							 .Given(APost)
							 .When(ACommentIsAddedWithAnEmptyName)
							 .Then(ANameEmptyErrorShouldBeRaised)

						.WithScenario("Empty Comment")
							 .Given(APost)
							 .When(ACommentIsAddedThatIsEmpty)
							 .Then(ACommentIsEmptyErrorShouldBeRaised)
			 .Execute();
	}

	Guid id = Guid.NewGuid();
	Guid commentId = Guid.NewGuid();
	private void APost()
	{
		if (null == Repositories.PublishedPosts.Get(id))
		{
			_.Receive(new CreatePost() { Id = id, SessionId = _.SessionId, Title = id.ToString(), WhenCreated = DateTime.Now });
			_.Receive(new EditPost() { Id = id, SessionId = _.SessionId, Content = id.ToString(), WhenEdited = DateTime.Now });
			_.Receive(new PublishPost() { Id = id, SessionId = _.SessionId, WhenPublished = DateTime.Now });
		}
	}

	private void ACommentIsAdded()
	{
		_.Receive(new AddCommentToPost() { Id = id, CommentId = commentId, Name = "name", EmailHash = string.Join("", new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes("email".ToLower().Trim())).Select(b => b.ToString("x2"))), Comment = "comment", WhenCommented = new DateTime(2000, 1, 1), Homepage = "homepage" });
	}

	private void TheCommentAppearsWithThePost()
	{
		Assert.IsTrue(Repositories.Comments.GetForPost(id).Any(c => c.Id == commentId));
	}

	private void ItHasTheCommentersName()
	{
		Assert.AreEqual("name", Repositories.Comments.Get(commentId).Name);
	}

	private void ItHasTheCommentersEmailHash()
	{
		Assert.AreEqual(string.Join("", new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes("email".ToLower().Trim())).Select(b => b.ToString("x2"))), Repositories.Comments.Get(commentId).EmailHash);
	}

	private void ItHasTheCommentersHomepage()
	{
		Assert.AreEqual("homepage", Repositories.Comments.Get(commentId).Homepage);
	}

	private void ItHasTheCommentersComment()
	{
		Assert.AreEqual("comment", Repositories.Comments.Get(commentId).CommentText);
	}

	private void ItHasTheDatetime()
	{
		Assert.AreEqual(new DateTime(2000, 1, 1), Repositories.Comments.Get(commentId).WhenCommented);
	}

	Exception emptyNameError;
	private void ACommentIsAddedWithAnEmptyName()
	{
		try
		{
			_.Receive(new AddCommentToPost() { Id = id, CommentId = commentId, Name = "", EmailHash = "email", Comment = "comment", WhenCommented = new DateTime(2000, 1, 1) });
		}
		catch (Exception ex)
		{
			emptyNameError = ex;
		}
	}

	private void ANameEmptyErrorShouldBeRaised()
	{
		Assert.IsInstanceOfType(emptyNameError, typeof(NameIsEmptyError));
	}

	Exception emptyCommentError;
	private void ACommentIsAddedThatIsEmpty()
	{
		try
		{
			_.Receive(new AddCommentToPost() { Id = id, CommentId = commentId, Name = "name", EmailHash = "email", Comment = "", WhenCommented = new DateTime(2000, 1, 1) });
		}
		catch (Exception ex)
		{
			emptyCommentError = ex;
		}
	}

	private void ACommentIsEmptyErrorShouldBeRaised()
	{
		Assert.IsInstanceOfType(emptyCommentError, typeof(CommentIsEmptyError));
	}
}
