using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class IdentifyAsBlogger_
{
    [TestMethod]
    public void IdentifyAsBlogger()
    {
        new Story("Identify as Blogger")
            .InOrderTo("restrict what can be done on the site")
            .AsA("Blogger")
            .IWant("to identify myself")

                    .WithScenario("Login")
                        .Given(Nothing)
                        .When(LoggedInWithTheCorrectUsernameAndPassword)
                        .Then(FunctionsThatOnlyABloggerCanDoCanBePerformed)

                    .WithScenario("Bad Login")
                        .Given(Nothing)
                        .When(LoggedInWithTheIncorrectUsernameOrPassword)
                        .Then(AWrongUsernameOrPasswordErrorIsRaised)

                    .WithScenario("Not Logged In")
                        .Given(BloggerIsNotLoggedIn)
                        .When(AnyCommandIsProcessedForTheBlogger)
                        .Then(ANotLoggedInErrorShouldBeRaised)
            .Execute();
    }

    private void Nothing()
    {
        throw new NotImplementedException();
    }

    private void LoggedInWithTheCorrectUsernameAndPassword()
    {
        throw new NotImplementedException();
    }

    private void FunctionsThatOnlyABloggerCanDoCanBePerformed()
    {
        throw new NotImplementedException();
    }

    private void LoggedInWithTheIncorrectUsernameOrPassword()
    {
        throw new NotImplementedException();
    }

    private void AWrongUsernameOrPasswordErrorIsRaised()
    {
        throw new NotImplementedException();
    }

    private void BloggerIsNotLoggedIn()
    {
        throw new NotImplementedException();
    }

    private void AnyCommandIsProcessedForTheBlogger()
    {
        throw new NotImplementedException();
    }

    private void ANotLoggedInErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }
}
