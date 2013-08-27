using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PopularPosts_
{
    [TestMethod]
    public void PopularPosts()
    {
        new Story("Popular Posts")
            .InOrderTo("work out what most people read and perhaps create ideas for what to write more about.")
            .AsA("Blogger")
            .IWant("to see what the most popular posts are.")

                    .WithScenario("Record Popular Posts")
                        .Given(APost)
                        .When(AReaderViewsThatPost)
                        .Then(TheNumberOfViewsForThatPostShouldBeIncremented)

                    .WithScenario("Display Popular Posts")
                        .Given(ANumberOfPostsHaveBeenViewed)
                        .When(PopularPostsAreShown)
                        .Then(ThePostsWithTheMostViewsShouldShownFirst)
            .Execute();
    }

    private void APost()
    {
        throw new NotImplementedException();
    }

    private void AReaderViewsThatPost()
    {
        throw new NotImplementedException();
    }

    private void TheNumberOfViewsForThatPostShouldBeIncremented()
    {
        throw new NotImplementedException();
    }

    private void ANumberOfPostsHaveBeenViewed()
    {
        throw new NotImplementedException();
    }

    private void PopularPostsAreShown()
    {
        throw new NotImplementedException();
    }

    private void ThePostsWithTheMostViewsShouldShownFirst()
    {
        throw new NotImplementedException();
    }
}
