using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SourceForAPost_
{
	[TestMethod]
	public void SourceForAPost()
	{
		new Story("Source for a Post")
			 .InOrderTo("remember where the idea for a Post came from.")
			 .AsA("Blogger")
			 .IWant("to record the source.")

						.WithScenario("Source of Post")
							 .Given(APost)
							 .When(ASourceIsAdded)
							 .Then(ThePostShouldHaveThatSource)
			 .Execute();
	}

	private void APost()
	{
		throw new NotImplementedException();
	}

	private void ASourceIsAdded()
	{
		throw new NotImplementedException();
	}

	private void ThePostShouldHaveThatSource()
	{
		throw new NotImplementedException();
	}
}
