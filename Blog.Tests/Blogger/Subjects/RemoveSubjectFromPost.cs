using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RemoveSubjectFromPost_
{
	[TestMethod]
	public void RemoveSubjectFromPost()
	{
		new Story("Remove Subject from Post")
			 .InOrderTo("fix a Subject erroneously attached to an Post")
			 .AsA("Blogger")
			 .IWant("to remove a Subject from an Idea/Post")

						.WithScenario("Remove Subject from Post")
							 .Given(ThereIsAPostWithASubject)
							 .When(TheSubjectIsRemovedFromTheIdea)
							 .Then(ThePostShouldNoLongerHaveTheSubject)
								  .And(TheSubjectShouldNoLongerHaveThatPost)
			 .Execute();
	}

	private void ThereIsAPostWithASubject()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectIsRemovedFromTheIdea()
	{
		throw new NotImplementedException();
	}

	private void ThePostShouldNoLongerHaveTheSubject()
	{
		throw new NotImplementedException();
	}

	private void TheSubjectShouldNoLongerHaveThatPost()
	{
		throw new NotImplementedException();
	}
}
