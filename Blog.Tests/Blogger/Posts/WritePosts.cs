using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using Yeast.EventStore;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using System.Threading;
using Blog.ReadModel.Repository;

[TestClass]
public class WritePosts_
{
	IConfigure config = Configure.With()
		.Synchrounous()
		.DebugLogger()
		.AzureEventStoreProvider("UseDevelopmentStorage=true")
		.JsonSerializer()
		.EventStore()
		.NoAggregateRootCache()
		.MessageReceiver()
			.Register<CreatePost, Post>()
		.EventPublisher()
			.Subscribe<DraftPostProjector, PostCreated>(DraftPostProjector.SubscriptionId);

	Guid id = Guid.NewGuid();
	IDraftPostRepository draftPostRepo = new DraftPostRepository();
	IPublishedPostRepository publishedPostRepo = new PublishedPostRepository();
		
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
                            .And(WithTheDateTimeItWasCreated)
                            .And(ItShouldNotBeSeenByReaders)

                    .WithScenario("Edit Post")
                        .Given(ADraftPost)
                        .When(ThePostIsEdited)
                        .Then(ThePostShouldBeUpdatedWithThenTheNewContents)
                            .And(WithTheDateTimeItWasEdited)
                            .And(ItShouldNotBeSeenByReaders2)

                    .WithScenario("Post Must Have Title")
                        .Given(Nothing)
                        .When(APostIsCreated)
                            .And(ThePostHasAnEmptyTitle)
                        .Then(APostMustHaveTitleErrorIsRaised)

                    .WithScenario("Post Must Have a unique URL based on Title")
                        .Given(Nothing)
                        .When(APostIsCreated)
                        .Then(TheURLForThePostShouldContainTheTitle)

                    .WithScenario("Two Posts with the same Title Should Not Have a URL Clash")
                        .Given(APostWithATitle)
                        .When(AnotherPostIsSavedWithTheSameTitle)
                        .Then(TheURLForThePostShouldContainTheTitle)
                            .And(ItShouldNotBeTheSameAsTheURLForTheExistingPost)
            .Execute();
    }

    private void Nothing()
    {
    }

    private void APostIsCreated()
    {
		 config.GetMessageReceiver.Receive(new CreatePost() { AggregateRootId = id, WhenCreated = new DateTime(2000, 1, 1) });
    }

    private void ItShouldAppearInTheListOfDraftPosts()
    {
		 Assert.IsTrue(draftPostRepo.Get().Any(dp => dp.Id == id));
    }

    private void WithTheDateTimeItWasCreated()
    {
		 Assert.AreEqual(new DateTime(2000, 1, 1), draftPostRepo.Get(id).WhenCreated);
    }

    private void ItShouldNotBeSeenByReaders()
    {
		 Assert.IsTrue(!publishedPostRepo.Get().Any(pp => pp.Id == id));
    }

    private void ADraftPost()
    {
        throw new NotImplementedException();
    }

    private void ThePostIsEdited()
    {
        throw new NotImplementedException();
    }

    private void ThePostShouldBeUpdatedWithThenTheNewContents()
    {
        throw new NotImplementedException();
    }

    private void WithTheDateTimeItWasEdited()
    {
        throw new NotImplementedException();
    }

	 private void ItShouldNotBeSeenByReaders2()
	 {
		 throw new NotImplementedException();
	 }

    private void ThePostHasAnEmptyTitle()
    {
        throw new NotImplementedException();
    }

    private void APostMustHaveTitleErrorIsRaised()
    {
        throw new NotImplementedException();
    }

    private void TheURLForThePostShouldContainTheTitle()
    {
        throw new NotImplementedException();
    }

    private void APostWithATitle()
    {
        throw new NotImplementedException();
    }

    private void AnotherPostIsSavedWithTheSameTitle()
    {
        throw new NotImplementedException();
    }
}
