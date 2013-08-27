using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommentIsntSpam_
{
    [TestMethod]
    public void CommentIsntSpam()
    {
        new Story("Comment Isn't Spam")
            .InOrderTo("show Comments accidentally marked as spam.")
            .AsA("Blogger")
            .IWant("to unmark Comments as spam.")

                    .WithScenario("Comment Isn't Spam")
                        .Given(ACommentThatIsMarkedAsSpam)
                        .When(TheCommentIsMarkedAsNotSpam)
                        .Then(TheCommentShouldBeShown)
            .Execute();
    }

    private void ACommentThatIsMarkedAsSpam()
    {
        throw new NotImplementedException();
    }

    private void TheCommentIsMarkedAsNotSpam()
    {
        throw new NotImplementedException();
    }

    private void TheCommentShouldBeShown()
    {
        throw new NotImplementedException();
    }
}
