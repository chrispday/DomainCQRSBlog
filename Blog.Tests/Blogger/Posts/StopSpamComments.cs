using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;

[TestClass]
public class StopSpamComments_
{
    [TestMethod]
    public void StopSpamComments()
    {
        new Story("Stop Spam Comments")
            .InOrderTo("stop spam Comments")
            .AsA("Blogger")
            .IWant("to mark a Comment as spam and hide it from Readers")
                .And("only allow comments that have passed a CAPTCHA")
                .And("POSSIBLY moderate the comments")

                    .WithScenario("Mark Comment As Spam")
                        .Given(APostWithAComment)
                        .When(ACommentIsMarkedAsSpam)
                        .Then(TheCommentIsNotDisplayedWithThePost)

                    .WithScenario("Pass a CAPTCHA to Post a Comment")
                        .Given(APost)
                        .When(ACommentIsAdded)
                            .And(TheCAPTCHAIsNotPassed)
                        .Then(TheCommentIsRejected)

                    .WithScenario("Moderate Comments")
                        .Given(APostAndCommentsAreToBeModerated)
                        .When(ACommentIsAdded)
                            .And(TheCommentIsntApproved)
                        .Then(TheCommentShouldNotBeShown)

                    .WithScenario("Approve Comment")
                        .Given(APostWithACommentThatIsntApproved)
                        .When(TheCommentIsApproved)
                        .Then(TheCommentShouldBeShown)
            .Execute();
    }

    private void APostWithAComment()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsMarkedAsSpam()
    {
        throw new NotImplementedException();
    }

    private void TheCommentIsNotDisplayedWithThePost()
    {
        throw new NotImplementedException();
    }

	 private void APost()
    {
		 throw new NotImplementedException();
	 }

    private void ACommentIsAdded()
    {
		 throw new NotImplementedException();
	 }

    private void TheCAPTCHAIsNotPassed()
    {
		 throw new NotImplementedException();
	 }

    private void TheCommentIsRejected()
    {
		 throw new NotImplementedException();
	 }

    private void APostAndCommentsAreToBeModerated()
    {
        throw new NotImplementedException();
    }

    private void TheCommentIsntApproved()
    {
        throw new NotImplementedException();
    }

    private void TheCommentShouldNotBeShown()
    {
        throw new NotImplementedException();
    }

    private void APostWithACommentThatIsntApproved()
    {
        throw new NotImplementedException();
    }

    private void TheCommentIsApproved()
    {
        throw new NotImplementedException();
    }

    private void TheCommentShouldBeShown()
    {
        throw new NotImplementedException();
    }
}
