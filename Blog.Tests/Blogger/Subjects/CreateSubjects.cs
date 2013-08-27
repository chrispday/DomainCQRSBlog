using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CreateSubjects_
{
    [TestMethod]
    public void CreateSubjects()
    {
        new Story("Create Subjects")
            .InOrderTo("group Posts.")
            .AsA("Blogger")
            .IWant("to create Subjects.")

                    .WithScenario("New Subject")
                        .Given(Nothing)
                        .When(ANewSubjectIsCreated)
                        .Then(ItShouldAppearInAListOfSubjects)

                    .WithScenario("Duplicate Subject")
                        .Given(AnExistingSubject)
                        .When(ANewSubjectIsCreatedWithTheSameName)
                        .Then(ADuplicateSubjectErrorShouldBeRaised)

                    .WithScenario("Empty Subject")
                        .Given(Nothing)
                        .When(ANewSubjectIsCreatedWithAnEmptyName)
                        .Then(AnEmptySubjectErrorShouldBeRaised)
            .Execute();
    }

    private void Nothing()
    {
        throw new NotImplementedException();
    }

    private void ANewSubjectIsCreated()
    {
        throw new NotImplementedException();
    }

    private void ItShouldAppearInAListOfSubjects()
    {
        throw new NotImplementedException();
    }

    private void AnExistingSubject()
    {
        throw new NotImplementedException();
    }

    private void ANewSubjectIsCreatedWithTheSameName()
    {
        throw new NotImplementedException();
    }

    private void ADuplicateSubjectErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }

    private void ANewSubjectIsCreatedWithAnEmptyName()
    {
        throw new NotImplementedException();
    }

    private void AnEmptySubjectErrorShouldBeRaised()
    {
        throw new NotImplementedException();
    }
}
