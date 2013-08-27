using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class EditSubjects_
{
	[TestMethod]
	public void EditSubjects()
	{
		new Story("Edit Subjects")
			 .InOrderTo("change the name of a Subject.")
			 .AsA("Blogger")
			 .IWant("to edit Subjects.")

						.WithScenario("Subject Edited")
							 .Given(AnExistingSubject)
								  .And(ThatSubjectIsAttachedToPosts)
							 .When(TheSubjectIsChanged)
							 .Then(TheChangedSubjectShouldAppearInAListOfSubjects)
								  .And(AllRelatedPostsAlsoShowTheChangedSubject)

						.WithScenario("Changed to Empty Subject")
							 .Given(AnExistingSubject)
							 .When(TheSubjectIsChangedToAnEmptyName)
							 .Then(AnEmptySubjectErrorShouldBeRaised)

						.WithScenario("Changed to Same Name as Another Subject")
							 .Given(TwoExistingSubjects)
							 .When(TheSubjectIsChangedSoThatTheNameIsTheSame)
							 .Then(TheRelatedPostsShouldBeAddedToTheExistingSubject)
			 .Execute();
	}

	private void AnExistingSubject()
	{
		throw new NotImplementedException();
	}

	private void ThatSubjectIsAttachedToPosts()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectIsChanged()
	{
		throw new NotImplementedException();
	}

	private void TheChangedSubjectShouldAppearInAListOfSubjects()
	{
		throw new NotImplementedException();
	}

	private void AllRelatedPostsAlsoShowTheChangedSubject()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectIsChangedToAnEmptyName()
	{
		throw new NotImplementedException();
	}

	private void AnEmptySubjectErrorShouldBeRaised()
	{
		throw new NotImplementedException();
	}

	private void TwoExistingSubjects()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectIsChangedSoThatTheNameIsTheSame()
	{
		throw new NotImplementedException();
	}

	private void TheRelatedPostsShouldBeAddedToTheExistingSubject()
	{
		throw new NotImplementedException();
	}
}
