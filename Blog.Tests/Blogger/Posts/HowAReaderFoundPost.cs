using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class HowAReaderFoundPost_
{
    [TestMethod]
    public void HowAReaderFoundPost()
    {
        new Story("How A Reader Found Post (Refback)")
            .InOrderTo("follow back to other pages to see what has been written about my Post.")
            .AsA("Blogger")
            .IWant("to know how a Reader found a Post.")

                    .WithScenario("track referrer")
                        .Given(APost)
                        .When(AReaderIsReferredFromAnotherPage)
                        .Then(ThatReferrerIsAddedToThePost)
            .Execute();
    }

    private void APost()
    {
        throw new NotImplementedException();
    }

    private void AReaderIsReferredFromAnotherPage()
    {
        throw new NotImplementedException();
    }

    private void ThatReferrerIsAddedToThePost()
    {
        throw new NotImplementedException();
    }
}
