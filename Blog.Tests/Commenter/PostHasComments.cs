using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using Blog.Tests;
using Blog.ReadModel.Repository;
using Blog.ReadModel.Data;

[TestClass]
public class PostHasComments_
{
	[TestMethod]
	public void PostHasComments()
	{
		new Story("Post has Comments")
			 .InOrderTo("know if there are any Comments to read.")
			 .AsA("Commenter")
			 .IWant("to see if a Post has Comments.")

						.WithScenario("Post has Conments")
							 .Given(APostWithComments)
							 .When(ThePostIsViewed)
							 .Then(TheNumberOfConmentsShouldBeShown)
			 .Execute();
	}

	Guid postId = Guid.NewGuid();
	private void APostWithComments()
	{
		_.Receive(new CreatePost() { Id = postId, SessionId = _.SessionId, Title = postId.ToString(), WhenCreated = DateTime.Now });
		_.Receive(new EditPost() { Id = postId, SessionId = _.SessionId, Content = postId.ToString(), WhenEdited = DateTime.Now });
		_.Receive(new PublishPost() { Id = postId, SessionId = _.SessionId, WhenPublished = DateTime.Now });

		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 3), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId =Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 5), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 2), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 4), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 1), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
		_.Receive(new AddCommentToPost() { Id = postId, WhenCommented = new DateTime(2000, 1, 6), Name = "name", Comment = "comment", EmailHash = "email", Homepage = "homepage", CommentId = Guid.NewGuid() });
	}

	PublishedPost post;
	private void ThePostIsViewed()
	{
		post = Repositories.PublishedPosts.Get(postId);
	}

	private void TheNumberOfConmentsShouldBeShown()
	{
		Assert.AreEqual(6, post.TotalComments);
	}
}
