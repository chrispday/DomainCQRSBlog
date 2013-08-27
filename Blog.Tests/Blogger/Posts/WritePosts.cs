using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class WritePosts_
{
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
                            .And(ItShouldNotBeSeenByReaders)

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
        throw new NotImplementedException();
    }

    private void APostIsCreated()
    {
        throw new NotImplementedException();
    }

    private void ItShouldAppearInTheListOfDraftPosts()
    {
        throw new NotImplementedException();
    }

    private void WithTheDateTimeItWasCreated()
    {
        throw new NotImplementedException();
    }

    private void ItShouldNotBeSeenByReaders()
    {
        throw new NotImplementedException();
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

    private void ItShouldNotBeTheSameAsTheURLForTheExistingPost()
    {
        throw new NotImplementedException();
    }
}
