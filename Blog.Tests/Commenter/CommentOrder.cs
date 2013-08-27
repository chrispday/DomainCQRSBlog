using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommentOrder_
{
	[TestMethod]
	public void CommentOrder()
	{
		new Story("Comment Order")
			 .InOrderTo("see the context of a comment without quoting it.")
			 .AsA("Commenter")
			 .IWant("to see comments in oldest first order.")

						.WithScenario("Comment are Oldest First")
							 .Given(APostWithComments)
							 .When(ThePostIsViewed)
							 .Then(TheCommentsShouldBeInOldestFirstOrder)
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

	private void TheCommentsShouldBeInOldestFirstOrder()
	{
		throw new NotImplementedException();
	}
}
