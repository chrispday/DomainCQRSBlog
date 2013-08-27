using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class NewComments_
{
	[TestMethod]
	public void NewComments()
	{
		new Story("New Comments")
			 .InOrderTo("read and respond to Comments without having to look through each post.")
			 .AsA("Commenter")
			 .IWant("to know if there are new comments on a post.")

						.WithScenario("Posts with New Comments")
							 .Given(SomePostsWithSomeComments)
							 .When(Nothing)
							 .Then(PostsShouldBeShownInOrderOfWhichHasMostRecentlyBeenCommentedOn)
								  .And(TheyArePaginated)
			 .Execute();
	}

	private void SomePostsWithSomeComments()
	{
		throw new NotImplementedException();
	}

	private void Nothing()
	{
		throw new NotImplementedException();
	}

	private void PostsShouldBeShownInOrderOfWhichHasMostRecentlyBeenCommentedOn()
	{
		throw new NotImplementedException();
	}

	private void TheyArePaginated()
	{
		throw new NotImplementedException();
	}
}
