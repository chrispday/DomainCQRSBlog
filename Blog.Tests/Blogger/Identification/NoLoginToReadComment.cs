using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class NoLoginToReadComment_
{
    [TestMethod]
    public void NoLoginToReadComment()
    {
        new Story("No Login to Read/Comment")
            .InOrderTo("make it easier for anyone to Read/Comment")
            .AsA("Reader/Commenter")
            .IWant("to not have to login.")

                    .WithScenario("No Login")
                        .Given(Nothing)
                        .When(AnyCommandIsProcessedForAReaderCommenter)
                            .And(NotLoggedIn)
                        .Then(NoErrorShouldBeRaised)
            .Execute();
    }

    private void Nothing()
    {
        throw new NotImplementedException();
    }

    private void AnyCommandIsProcessedForAReaderCommenter()
    {
        throw new NotImplementedException();
    }

    private void NotLoggedIn()
    {
        throw new NotImplementedException();
    }

    private void NoErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }
}
