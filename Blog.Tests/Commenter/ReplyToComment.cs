using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReplyToComment_
{
	[TestMethod]
	public void ReplyToComment()
	{
		new Story("Reply to Comment")
			 .InOrderTo("respond to a particular Comment.")
			 .AsA("Commenter")
			 .IWant("to reply to a Comment.")

						.WithScenario("")
							 .Given(APostWithComments)
							 .When(ACommentIsRepliedTo)
							 .Then(TheCommentThatIsBeingRepliedToIsSaved)
								  .And(ItPassesTheSameValidationsForUsualComments)
			 .Execute();
	}

	private void APostWithComments()
	{
		throw new NotImplementedException();
	}

	private void ACommentIsRepliedTo()
	{
		throw new NotImplementedException();
	}

	private void TheCommentThatIsBeingRepliedToIsSaved()
	{
		throw new NotImplementedException();
	}

	private void ItPassesTheSameValidationsForUsualComments()
	{
		throw new NotImplementedException();
	}
}
