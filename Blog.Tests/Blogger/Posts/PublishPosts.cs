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

						  .WithScenario("Post Must Have a unique URL based on Title")
								.Given(Nothing)
								.When(APostIsCreated)
								.Then(TheURLForThePostShouldContainTheTitle)

						  .WithScenario("Two Published Posts with the same Title Should Not Have a URL Clash")
								.Given(APublishedPostWithATitle)
								.When(AnotherPostIsPublishedWithTheSameTitle)
								.Then(TheURLForThePostShouldContainTheTitle2)
									 .And(ItShouldNotBeTheSameAsTheURLForTheExistingPost)
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

	 private void Nothing()
	 {
		 throw new NotImplementedException();
	 }

	 private void APostIsCreated()
	 {
		 throw new NotImplementedException();
	 }

	 private void TheURLForThePostShouldContainTheTitle()
	 {
		 throw new NotImplementedException();
		 //var url = new string(createdTitle.Select(c => Char.IsLetter(c) ? c : '-').ToArray());
		 //Assert.AreEqual(url, Repositories.DraftPosts.Get(createdId).Url);
	 }

	 private void APublishedPostWithATitle()
	 {
		 throw new NotImplementedException();
	 }

	 private void AnotherPostIsPublishedWithTheSameTitle()
	 {
		 throw new NotImplementedException();
	 }

	 private void TheURLForThePostShouldContainTheTitle2()
	 {
		 throw new NotImplementedException();
	 }

	 private void ItShouldNotBeTheSameAsTheURLForTheExistingPost()
	 {
		 throw new NotImplementedException();
	 }
}
