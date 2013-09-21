using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using DomainCQRS;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using System.Threading;
using Blog.ReadModel.Repository;
using Blog.Domain.Errors;
using Blog.Tests;

[TestClass]
public class WritePosts_
{
	Guid createdId = Guid.NewGuid();
	string createdTitle = "Created title " + Guid.NewGuid().ToString();

	Guid draftId = Guid.NewGuid();
	string draftContent = "Draft content " + Guid.NewGuid().ToString();
	string draftTitle = "Draft title " + Guid.NewGuid().ToString();

	Guid draftContentId = Guid.NewGuid();
	string draftContent2 = "Draft content " + Guid.NewGuid().ToString();

	Guid createdIdNoTitle = Guid.NewGuid();
	Exception exceptionNoTitle;

	[TestMethod]
	public void WritePosts()
	{
		new Story("Write Posts")
			 .InOrderTo("write and refine posts.")
			 .AsA("Blogger")
			 .IWant("to edit drafts of a Post.")

						.WithScenario("Create Post")
							 .Given(Nothing)
							 .When(APostIsCreated)
							 .Then(ItShouldAppearInTheListOfDraftPosts)
								 .And(WithTheTitleGiven)
								  .And(WithTheDateTimeItWasCreated)
								  .And(TheDateTimeEditedIsTheSameAsWhenItWasCreated)
								  .And(ItShouldNotBeSeenByReaders, createdId)

						.WithScenario("Edit Post")
							 .Given(ADraftPost, draftId, draftTitle)
							 .When(ThePostIsEdited)
							 .Then(ThePostShouldBeUpdatedWithTheNewContents, draftId, draftContent)
								 .And(ThePostShouldBeUpdatedWithTheNewTitle)
								  .And(WithTheDateTimeItWasEdited, draftId, new DateTime(2001, 1, 1))
								  .And(ItShouldNotBeSeenByReaders, draftId)

						.WithScenario("Edit Post Content")
							 .Given(ADraftPost, draftContentId, draftTitle)
							 .When(ThePostContentIsEdited)
							 .Then(ThePostShouldBeUpdatedWithTheNewContents, draftContentId, draftContent2)
								 .And(ThePostTitleShouldNotBeUpdated)
								  .And(WithTheDateTimeItWasEdited, draftContentId, new DateTime(2002, 1, 1))
								  .And(ItShouldNotBeSeenByReaders, draftContentId)

						.WithScenario("Post Must Have Title")
							 .Given(Nothing)
							 .When(APostIsCreatedWithAnEmptyTitle)
							 .Then(APostMustHaveTitleErrorIsRaised)
			 .Execute();
	}

	private void Nothing()
	{
	}

	private void APostIsCreated()
	{
		_.Receive(new CreatePost() { Id = createdId, WhenCreated = new DateTime(2000, 1, 1), Title = createdTitle, SessionId = _.SessionId });
	}

	private void ItShouldAppearInTheListOfDraftPosts()
	{
		Assert.IsTrue(Repositories.DraftPosts.Get().Any(dp => dp.Id == createdId));
	}

	private void WithTheTitleGiven()
	{
		Assert.AreEqual(createdTitle, Repositories.DraftPosts.Get(createdId).Title);
	}

	private void WithTheDateTimeItWasCreated()
	{
		Assert.AreEqual(new DateTime(2000, 1, 1), Repositories.DraftPosts.Get(createdId).WhenCreated);
	}

	private void TheDateTimeEditedIsTheSameAsWhenItWasCreated()
	{
		Assert.AreEqual(new DateTime(2000, 1, 1), Repositories.DraftPosts.Get(createdId).WhenEdited);
	}

	private void ItShouldNotBeSeenByReaders(Guid arg1)
	{
		Assert.IsTrue(!Repositories.PublishedPosts.Get().Any(pp => pp.Id == arg1));
	}

	private void ADraftPost(Guid id, string title)
	{
		_.Receive(new CreatePost() { Id = id, WhenCreated = DateTime.Now, Title = title, SessionId = _.SessionId });
	}

	private void ThePostIsEdited()
	{
		_.Receive(new EditPost() { Id = draftId, Content = draftContent, WhenEdited = new DateTime(2001, 1, 1), Title = draftTitle, SessionId = _.SessionId });
	}

	private void ThePostShouldBeUpdatedWithTheNewContents(Guid id, string content)
	{
		Assert.AreEqual(content, Repositories.DraftPosts.Get(id).Content);
	}

	private void ThePostShouldBeUpdatedWithTheNewTitle()
	{
		Assert.AreEqual(draftTitle, Repositories.DraftPosts.Get(draftId).Title);
	}

	private void WithTheDateTimeItWasEdited(Guid id, DateTime dt)
	{
		Assert.AreEqual(dt, Repositories.DraftPosts.Get(id).WhenEdited);
	}

	private void ThePostContentIsEdited()
	{
		_.Receive(new EditPost() { Id = draftContentId, Content = draftContent2, WhenEdited = new DateTime(2002, 1, 1), SessionId = _.SessionId });
	}

	private void ThePostTitleShouldNotBeUpdated()
	{
		Assert.AreEqual(draftTitle, Repositories.DraftPosts.Get(draftContentId).Title);
	}

	private void APostIsCreatedWithAnEmptyTitle()
	{
		try
		{
			_.Receive(new CreatePost() { Id = createdIdNoTitle, WhenCreated = DateTime.Now, SessionId = _.SessionId });
		}
		catch (Exception ex)
		{
			exceptionNoTitle = ex;
		}
	}

	private void APostMustHaveTitleErrorIsRaised()
	{
		Assert.IsInstanceOfType(exceptionNoTitle, typeof(PostMustHaveTitleError));
	}
}
