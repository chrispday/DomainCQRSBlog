using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommentOnPost_
{
    [TestMethod]
    public void CommentOnPost()
    {
        new Story("Comment on Post")
            .InOrderTo("provide feedback OR ask a question OR answer a question.")
            .AsA("Commenter")
            .IWant("to comment on Posts.")

                    .WithScenario("Comment on Post")
                        .Given(APost)
                        .When(ACommentIsAdded)
                        .Then(TheCommentAppearsWithThePost)
                            .And(ItHasTheCommentersName)
                            .And(ItHasTheCommentersComment)
                            .And(ItHasTheDatetime)

                    .WithScenario("Empty Commenter Name")
                        .Given(APost)
                        .When(ACommentIsAddedWithAnEmptyName)
                        .Then(ANameEmptyErrorShouldBeRaised)

                    .WithScenario("Empty Comment")
                        .Given(APost)
                        .When(ACommentIsAddedThatIsEmpty)
                        .Then(ACommentIsEmptyErrorShouldBeRaised)
            .Execute();
    }

    private void APost()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsAdded()
    {
        throw new NotImplementedException();
    }

    private void TheCommentAppearsWithThePost()
    {
        throw new NotImplementedException();
    }

    private void ItHasTheCommentersName()
    {
        throw new NotImplementedException();
    }

    private void ItHasTheCommentersComment()
    {
        throw new NotImplementedException();
    }

    private void ItHasTheDatetime()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsAddedWithAnEmptyName()
    {
        throw new NotImplementedException();
    }

    private void ANameEmptyErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsAddedThatIsEmpty()
    {
        throw new NotImplementedException();
    }

    private void ACommentIsEmptyErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }
}
