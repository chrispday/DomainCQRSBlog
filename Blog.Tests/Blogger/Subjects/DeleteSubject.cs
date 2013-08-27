using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DeleteSubject_
{
    [TestMethod]
    public void DeleteSubject()
    {
        new Story("Delete Subject")
            .InOrderTo("remove unused Subjects")
            .AsA("Blogger")
            .IWant("to delete Subjects")

                    .WithScenario("Delete a Subject")
                        .Given(ASubjectWithNoRelatedPosts)
                        .When(ThatSubjectIsDeleted)
                        .Then(ItShouldNotAppearInTheListOfSubjects)

                    .WithScenario("Delete Subject with Posts")
                        .Given(ASubjectWithARelatedPost)
                        .When(ThatSubjectIsDeleted)
                        .Then(ASubjectHasPostsErrorShouldBeRaised)
            .Execute();
    }

    private void ASubjectWithNoRelatedPosts()
    {
        throw new NotImplementedException();
    }

    private void ThatSubjectIsDeleted()
    {
        throw new NotImplementedException();
    }

    private void ItShouldNotAppearInTheListOfSubjects()
    {
        throw new NotImplementedException();
    }

    private void ASubjectWithARelatedPost()
    {
        throw new NotImplementedException();
    }

    private void ASubjectHasPostsErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }
}
