using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EditPublishedPosts_
{
    [TestMethod]
    public void EditPublishedPosts()
    {
        new Story("Edit Published Posts")
            .InOrderTo("make changes and leave Posts in a state that Readers won’t see.")
            .AsA("Blogger")
            .IWant("to edit published Posts as a draft")
                .And("leave Reader’s seeing the unchanged Post")
                .And("wait until I publish the draft to show the changed Post to Readers")
                .And("it should appear with the original publish date")

                    .WithScenario("Edit Published Post")
                        .Given(APublishedPost)
                        .When(ThePostIsEdited)
                        .Then(ADraftVersionIfThePostIsCreated)
                            .And(TheDraftIsNotSeenByReaders)
                            .And(ThePublishedVersionOfThePostIsSeenByReaders)

                    .WithScenario("Publish Updated Post")
                        .Given(APublishedPostThatHasBeenEdited)
                        .When(ThePostIsPublished)
                        .Then(TheUpdatedVersionOfThePostShouldBeSeenByReaders)
                            .And(ThePublishedDatetimeShouldRemainTheSameAsWhenFirstPublished)
            .Execute();
    }

    private void APublishedPost()
    {
        throw new NotImplementedException();
    }

    private void ThePostIsEdited()
    {
        throw new NotImplementedException();
    }

    private void ADraftVersionIfThePostIsCreated()
    {
        throw new NotImplementedException();
    }

    private void TheDraftIsNotSeenByReaders()
    {
        throw new NotImplementedException();
    }

    private void ThePublishedVersionOfThePostIsSeenByReaders()
    {
        throw new NotImplementedException();
    }

    private void APublishedPostThatHasBeenEdited()
    {
        throw new NotImplementedException();
    }

    private void ThePostIsPublished()
    {
        throw new NotImplementedException();
    }

    private void TheUpdatedVersionOfThePostShouldBeSeenByReaders()
    {
        throw new NotImplementedException();
    }

    private void ThePublishedDatetimeShouldRemainTheSameAsWhenFirstPublished()
    {
        throw new NotImplementedException();
    }
}
