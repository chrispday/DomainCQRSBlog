using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using Blog.Tests;
using Blog.ReadModel.Repository;
using Blog.ReadModel.Data;

[TestClass]
public class ReadAPost_
{
	[TestMethod]
	public void ReadAPost()
	{
		new Story("Read a Post")
			 .InOrderTo("read a Post.")
			 .AsA("Reader")
			 .IWant("to see a post")

						.WithScenario("Read a Post")
							 .Given(APublishedPost)
							 .When(APostIsAskedForByUrl)
							 .Then(TheReaderCanReadThePost)
			 .Execute();
	}

	Guid id = Guid.NewGuid();
	private void APublishedPost()
	{
		_.Receive(new CreatePost() { Id = id, WhenCreated = DateTime.Now, Title = "Title" + id.ToString(), SessionId = _.SessionId });
		_.Receive(new EditPost() { Id = id, WhenEdited = DateTime.Now, Content = "Content" + id.ToString(), SessionId = _.SessionId });
		_.Receive(new PublishPost() { Id = id, WhenPublished = DateTime.Now, SessionId = _.SessionId });
	}

	PublishedPost post;
	private void APostIsAskedForByUrl()
	{
		post = Repositories.PublishedPosts.GetByUrl(Repositories.PublishedPosts.Get(id).Url);
	}

	private void TheReaderCanReadThePost()
	{
		Assert.AreEqual(id, post.Id);
	}
}
