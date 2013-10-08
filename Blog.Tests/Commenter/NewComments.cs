using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Data;
using Blog.ReadModel.Repository;
using System.Collections.Generic;
using System.Threading;

[TestClass]
public class NewComments_
{
	[TestMethod]
	public void NewComments()
	{
		new Story("New Comments")
			 .InOrderTo("read and respond to Comments without having to look through each post.")
			 .AsA("Commenter")
			 .IWant("to know if there are new comments on a post.")

						.WithScenario("Posts with New Comments")
							 .Given(SomePostsWithSomeComments)
							 .When(Nothing)
							 .Then(PostsShouldBeShownInOrderOfWhichHasMostRecentlyBeenCommentedOn)
								  .And(TheyArePaginated)
			 .Execute();
	}

	private void SomePostsWithSomeComments()
	{
		CreatePostAndComments();
		Thread.Sleep(100);
		CreatePostAndComments();
		Thread.Sleep(100);
		CreatePostAndComments();
		Thread.Sleep(100);
		CreatePostAndComments();
		Thread.Sleep(100);
		CreatePostAndComments();
		Thread.Sleep(100);
		CreatePostAndComments();
	}

	private void CreatePostAndComments()
	{
		var id = Guid.NewGuid();
		_.Receive(new CreatePost() { Id = id, SessionId = _.SessionId, Title = id.ToString(), WhenCreated = DateTime.Now });
		_.Receive(new EditPost() { Id = id, SessionId = _.SessionId, Content = id.ToString(), WhenEdited = DateTime.Now });
		_.Receive(new PublishPost() { Id = id, SessionId = _.SessionId, WhenPublished = DateTime.Now });
		_.Receive(new AddCommentToPost() { Id = id, CommentId = Guid.NewGuid(), Name = id.ToString(), EmailHash = id.ToString(), Comment = id.ToString(), Homepage = "homepage", WhenCommented = DateTime.Now });
	}

	private void Nothing()
	{
	}

	IEnumerable<PublishedPost> posts;
	private void PostsShouldBeShownInOrderOfWhichHasMostRecentlyBeenCommentedOn()
	{
		posts = Repositories.PublishedPosts.GetPagedPostsByMostRecentComments(1, 5, true);
		var when = posts.First().MostRecentCommentWhen;
		foreach (var post in posts.Skip(1))
		{
			Assert.IsTrue(post.MostRecentCommentWhen < when);
			when = post.MostRecentCommentWhen;
		}
	}

	private void TheyArePaginated()
	{
		Assert.AreEqual(6, posts.Count());
	}
}
