using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Repository;
using Blog.Domain.Errors;

[TestClass]
public class PublishPosts_
{
	Exception publishEx;
	
	[TestMethod]
    public void PublishPosts()
    {
		 Guid unpublishedId = Guid.NewGuid();
		 Guid unpublishedIdNoContent = Guid.NewGuid();
		 Guid publishedId = Guid.NewGuid();
		 Guid publishedIdAnother = Guid.NewGuid();
		 Guid unpublishedIdUrl = Guid.NewGuid();
		 
		 new Story("Publish Posts")
            .InOrderTo("make the current draft of the Post available to Readers.")
            .AsA("Blogger")
            .IWant("to publish Posts.")

                    .WithScenario("Publish Post")
								.Given(AnUnpublishedPost, unpublishedId)
									.And(WithTheContent, unpublishedId, "Content" + unpublishedId.ToString())
								.When(ThePostIsPublished, unpublishedId)
								.Then(ItShouldBeAvailableToReaders, unpublishedId)
									 .And(WithTheDatetimeIsWasPublished, unpublishedId)

                    .WithScenario("Published Post Must Have Content")
								.Given(AnUnpublishedPost, unpublishedIdNoContent)
									 .And(ThePostContentIsEmpty, unpublishedIdNoContent)
								.When(ThePostIsPublishedWithoutContent, unpublishedIdNoContent)
                        .Then(APostMustHaveContentErrorIsRaised)

						  .WithScenario("Post Must Have a unique URL based on Title")
								.Given(AnUnpublishedPost, unpublishedIdUrl)
									.And(WithTheContent, unpublishedIdUrl, "Content" + unpublishedIdUrl.ToString())
								.When(ThePostIsPublished, unpublishedIdUrl)
								.Then(TheUrlForThePostShouldContainTheTitle, unpublishedIdUrl, unpublishedIdUrl)

						  .WithScenario("Two Published Posts with the same Title Should Not Have a URL Clash")
								.Given(APublishedPostWithATitle, publishedId)
								.When(AnotherPostIsPublishedWithTheSameTitle, publishedIdAnother, publishedId)
								.Then(TheUrlForThePostShouldContainTheTitle, publishedIdAnother, publishedId)
									 .And(ItShouldNotBeTheSameAsTheUrlForTheExistingPost, publishedId, publishedIdAnother)
				.Execute();
    }

    private void AnUnpublishedPost(Guid id)
    {
		 _.Config.GetMessageReceiver.Receive(new CreatePost() { Id = id, Title = "Title" + id.ToString(), WhenCreated = DateTime.Now });
    }

	 private void WithTheContent(Guid id, string content)
	 {
		 _.Config.GetMessageReceiver.Receive(new EditPost() { Id = id, WhenEdited = DateTime.Now, Content = content });
	 }

	 private void ThePostIsPublished(Guid id)
    {
		 _.Config.GetMessageReceiver.Receive(new PublishPost() { Id = id, WhenPublished = new DateTime(2000, 1, 1) });
	 }

	 private void ItShouldBeAvailableToReaders(Guid id)
    {
		 Assert.IsTrue(Repositories.PublishedPosts.Get().Any(p => p.Id == id));
    }

	 private void WithTheDatetimeIsWasPublished(Guid id)
    {
		 Assert.AreEqual(new DateTime(2000, 1, 1), Repositories.PublishedPosts.Get(id).WhenPublished);
    }

	 private void ThePostContentIsEmpty(Guid id)
    {
		 _.Config.GetMessageReceiver.Receive(new EditPost() { Id = id, WhenEdited = DateTime.Now, Content = "" });
    }

	private void ThePostIsPublishedWithoutContent(Guid id)
	{
		try
		{
			_.Config.GetMessageReceiver.Receive(new PublishPost() { Id = id, WhenPublished = new DateTime(2000, 1, 1) });
		}
		catch (Exception ex)
		{
			publishEx = ex;
		}
	}

	 private void APostMustHaveContentErrorIsRaised()
    {
		 Assert.IsInstanceOfType(publishEx, typeof(PostMustHaveContentError));
    }

	 private void TheUrlForThePostShouldContainTheTitle(Guid id, Guid titleId)
	 {
		 var title = new string(("Title" + titleId.ToString()).Select(c => Char.IsLetterOrDigit(c) ? c : '-').ToArray());
		 Assert.IsTrue(Repositories.PublishedPosts.Get(id).Url.Contains(title), "{0} contains {1}", Repositories.PublishedPosts.Get(id).Url, title);
	 }

	 private void APublishedPostWithATitle(Guid id)
	 {
		 _.Config.GetMessageReceiver.Receive(new CreatePost() { Id = id, WhenCreated = DateTime.Now, Title = "Title" + id.ToString() });
		 _.Config.GetMessageReceiver.Receive(new EditPost() { Id = id, WhenEdited = DateTime.Now, Content = "Content" + id.ToString() });
		 _.Config.GetMessageReceiver.Receive(new PublishPost() { Id = id, WhenPublished = DateTime.Now });
	 }

	 private void AnotherPostIsPublishedWithTheSameTitle(Guid id, Guid otherId)
	 {
		 _.Config.GetMessageReceiver.Receive(new CreatePost() { Id = id, WhenCreated = DateTime.Now, Title = "Title" + otherId.ToString() });
		 _.Config.GetMessageReceiver.Receive(new EditPost() { Id = id, WhenEdited = DateTime.Now, Content = "Content" + id.ToString() });
		 _.Config.GetMessageReceiver.Receive(new PublishPost() { Id = id, WhenPublished = DateTime.Now });
	 }

	 private void ItShouldNotBeTheSameAsTheUrlForTheExistingPost(Guid id, Guid id2)
	 {
		 Assert.AreNotEqual(Repositories.PublishedPosts.Get(id).Url, Repositories.PublishedPosts.Get(id2).Url);
	 }
}
