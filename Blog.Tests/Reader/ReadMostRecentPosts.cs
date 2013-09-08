using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Repository;
using System.Collections.Generic;
using System.Threading;
using Blog.ReadModel.Data;

[TestClass]
public class ReadMostRecentPosts_
{
	[TestMethod]
	public void ReadMostRecentPosts()
	{
		new Story("Read Most Recent Posts")
			 .InOrderTo("read the most recent posts first.")
			 .AsA("Reader")
			 .IWant("to see Posts ordered by most recent first.")

						.WithScenario("See Most Recent Posts")
							 .Given(SomePublishedPosts)
							 .When(TheMostRecentPostsAreAskedFor)
							 .Then(TheReaderCanViewPostsWithTheMostRecentFirst)
								  .And(TheyArePaginated)
			 .Execute();
	}

	Guid userId = Guid.NewGuid();
	Guid sessionId;
	private void SomePublishedPosts()
	{
		_.Receive(new CreateUser() { Id = userId, Password = userId.ToString(), Salt = userId, Username = userId.ToString() });
		_.Receive(new Login() { Username = userId.ToString(), Password = userId.ToString(), SessionId = userId } );

		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
		CreateAndPublishPost(Guid.NewGuid());
	}

	private void CreateAndPublishPost(Guid postId1)
	{
		_.Receive(new CreatePost() { Id = postId1, SessionId = userId, Title = postId1.ToString(), WhenCreated = DateTime.Now });
		_.Receive(new EditPost() { Id = postId1, SessionId = userId, Content = postId1.ToString(), WhenEdited = DateTime.Now });
		_.Receive(new PublishPost() { Id = postId1, SessionId = userId, WhenPublished = DateTime.Now });
		Thread.Sleep(100);
	}

	List<IEnumerable<PublishedPost>> posts = new List<IEnumerable<PublishedPost>>();
	int pagesize = 4;
	private void TheMostRecentPostsAreAskedFor()
	{
		while (true)
		{
			var p = Repositories.PublishedPosts.MostRecentPosts(posts.Count + 1, pagesize, true).ToList();
			posts.Add(p.Take(pagesize));
			if (p.Count() < pagesize + 1)
			{
				break;
			}
		}
	}

	private void TheReaderCanViewPostsWithTheMostRecentFirst()
	{
		var all = posts.SelectMany(p => p.Select(sp => sp)).ToList();
		for (int i = 0; i < all.Count - 1; i++)
		{
			Assert.IsTrue(all[i].WhenPublished >= all[i + 1].WhenPublished);
		}
	}

	private void TheyArePaginated()
	{
		foreach (var p in posts.Take(posts.Count - 1))
		{
			Assert.AreEqual(pagesize, p.Count());
		}
		Assert.IsTrue(pagesize >= posts.Last().Count());
	}
}
