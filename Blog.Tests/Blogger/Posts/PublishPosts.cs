using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PublishPosts_
{
    [TestMethod]
    public void PublishPosts()
    {
        new Story("Publish Posts")
            .InOrderTo("make the current draft of the Post available to Readers.")
            .AsA("Blogger")
            .IWant("to publish Posts.")

                    .WithScenario("Publish Post")
                        .Given(AnUnpublishedPost)
                        .When(ThePostIsPublished)
                        .Then(ItShouldBeAvailableToReaders)
                            .And(WithTheDatetimeIsWasPublished)

                    .WithScenario("Published Post Must Have a Subject")
                        .Given(AnUnpublishedPost)
                            .And(ThePostHasNoSubjects)
                        .When(ThePostIsPublished)
                        .Then(APostMustHaveSubjectsErrorIsRaised)

                    .WithScenario("Published Post Must Have Content")
                        .Given(AnUnpublishedPost)
                            .And(ThePostContentIsEmpty)
                        .When(ThePostIsPublished)
                        .Then(APostMustHaveContentErrorIsRaised)
            .Execute();
    }

    private void AnUnpublishedPost()
    {
        throw new NotImplementedException();
    }

    private void ThePostIsPublished()
    {
        throw new NotImplementedException();
    }

    private void ItShouldBeAvailableToReaders()
    {
        throw new NotImplementedException();
    }

    private void WithTheDatetimeIsWasPublished()
    {
        throw new NotImplementedException();
    }

    private void ThePostHasNoSubjects()
    {
        throw new NotImplementedException();
    }

    private void APostMustHaveSubjectsErrorIsRaised()
    {
        throw new NotImplementedException();
    }

    private void ThePostContentIsEmpty()
    {
        throw new NotImplementedException();
    }

    private void APostMustHaveContentErrorIsRaised()
    {
        throw new NotImplementedException();
    }
}
