using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EditComments_
{
    [TestMethod]
    public void EditComments()
    {
        new Story("Edit Comments")
            .InOrderTo("fix errors or typos, etc.. in comments")
            .AsA("Commenter")
            .IWant("to edit my Comments.")

                    .WithScenario("Edit Comment")
                        .Given(APostWithAComment)
                        .When(ACommentIsEdited)
                            .And(TheCommenterIsVerifiedToHaveWrittenTheComment)
                        .Then(TheCommentIsUpdated)
                            .And(TheDatetimeOfTheEditIsRecorded)
                            .And(TheCommentStillHasItsOriginalCommenterAndDatetimeForOrderingPurposes)
                            .And(ItPassesValidationsForUsualComments)
            .Execute();
    }

    private void APostWithAComment()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsEdited()
    {
        throw new NotImplementedException();
    }

    private void TheCommenterIsVerifiedToHaveWrittenTheComment()
    {
        throw new NotImplementedException();
    }

    private void TheCommentIsUpdated()
    {
        throw new NotImplementedException();
    }

    private void TheDatetimeOfTheEditIsRecorded()
    {
        throw new NotImplementedException();
    }

    private void TheCommentStillHasItsOriginalCommenterAndDatetimeForOrderingPurposes()
    {
        throw new NotImplementedException();
    }

    private void ItPassesValidationsForUsualComments()
    {
        throw new NotImplementedException();
    }
}
