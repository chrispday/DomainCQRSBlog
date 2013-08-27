using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PostsHaveSubjects_
{
	[TestMethod]
	public void PostsHaveSubjects()
	{
		new Story("Posts have Subjects")
			 .InOrderTo("group related Posts together.")
			 .AsA("Blogger")
			 .IWant("to attach Subjects to Posts.")

						.WithScenario("Subject for Post")
							 .Given(ThereIsAnPost)
								  .And(ASubject)
							 .When(ASubjectIsAttachedToAPost)
							 .Then(ThePostShouldHaveThatSubject)
								  .And(TheSubjectShouldHaveThatPost)
			 .Execute();
	}

	private void ThereIsAnPost()
	{
		throw new NotImplementedException();
	}

	private void ASubject()
	{
		throw new NotImplementedException();
	}

	private void ASubjectIsAttachedToAPost()
	{
		throw new NotImplementedException();
	}

	private void ThePostShouldHaveThatSubject()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectShouldHaveThatPost()
	{
		throw new NotImplementedException();
	}
}
