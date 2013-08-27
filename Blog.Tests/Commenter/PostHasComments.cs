using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PostHasComments_
{
	[TestMethod]
	public void PostHasComments()
	{
		new Story("Post has Comments")
			 .InOrderTo("know if there are any Comments to read.")
			 .AsA("Commenter")
			 .IWant("to see if a Post has Comments.")

						.WithScenario("Post has Conments")
							 .Given(APostWithComments)
							 .When(ThePostIsViewed)
							 .Then(TheNumberOfConmentsShouldBeShown)
			 .Execute();
	}

	private void APostWithComments()
	{
		throw new NotImplementedException();
	}

	private void ThePostIsViewed()
	{
		throw new NotImplementedException();
	}

	private void TheNumberOfConmentsShouldBeShown()
	{
		throw new NotImplementedException();
	}
}
