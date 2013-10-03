using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using Blog.Tests;
using System.Collections.Generic;
using Blog.ReadModel.Data;
using Blog.ReadModel.Repository;

[TestClass]
public class CommentOrder_
{
	[TestMethod]
	public void CommentOrder()
	{
		new Story("Comment Order")
			 .InOrderTo("see the context of a comment without quoting it.")
			 .AsA("Commenter")
			 .IWant("to see comments in oldest first order.")

						.WithScenario("Comment are Oldest First")
							 .Given(APostWithComments)
							 .When(ThePostIsViewed)
							 .Then(TheCommentsShouldBeInOldestFirstOrder)
							 .And(ThereShouldBeTheCorrectNumberOfComments)
			 .Execute();
	}

	Guid postId = Guid.NewGuid();
	private void APostWithComments()
	{
		_.Receive(new CreatePost() { Id = postId, SessionId = _.SessionId, Title = postId.ToString(), WhenCreated = DateTime.Now });
		_.Receive(new EditPost() { Id = postId, SessionId = _.SessionId, Content = postId.ToString(), WhenEdited = DateTime.Now });
		_.Receive(new PublishPost() { Id = postId, SessionId = _.SessionId, WhenPublished = DateTime.Now });

		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 3), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 5), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 2), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 4), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 1), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 6), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
	}

	IEnumerable<Comment> comments;
	private void ThePostIsViewed()
	{
		comments = Repositories.Comments.GetForPost(postId);
	}

	private void TheCommentsShouldBeInOldestFirstOrder()
	{
		var when = comments.First().WhenCommented;
		foreach (var comment in comments.Skip(1))
		{
			Assert.IsTrue(when < comment.WhenCommented);
			when = comment.WhenCommented;
		}
	}

	private void ThereShouldBeTheCorrectNumberOfComments()
	{
		Assert.AreEqual(6, comments.Count());
	}
}
