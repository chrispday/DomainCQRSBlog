using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RelatedPosts_
{
	[TestMethod]
	public void RelatedPosts()
	{
		new Story("Related Posts")
			 .InOrderTo("let Readers know that Posts are related")
			 .AsA("Blogger")
			 .IWant("to select previous Posts that are related.")

						.WithScenario("Add Related Post")
							 .Given(TwoExistingPosts)
							 .When(OnePostIsMarkedAsRelatedToTheOther)
							 .Then(ThePostsShouldAppearInEachOthersListOfRelatedPosts)

						.WithScenario("Remove Related Post")
							 .Given(TwoPostsThatAreRelated)
							 .When(OnePostHasTheRelationshipRemoved)
							 .Then(ThePostsShouldNotAppearInEachOthersListOfRelatedPosts)
			 .Execute();
	}

	private void TwoExistingPosts()
	{
		throw new NotImplementedException();
	}

	private void OnePostIsMarkedAsRelatedToTheOther()
	{
		throw new NotImplementedException();
	}

	private void ThePostsShouldAppearInEachOthersListOfRelatedPosts()
	{
		throw new NotImplementedException();
	}

	private void TwoPostsThatAreRelated()
	{
		throw new NotImplementedException();
	}

	private void OnePostHasTheRelationshipRemoved()
	{
		throw new NotImplementedException();
	}

	private void ThePostsShouldNotAppearInEachOthersListOfRelatedPosts()
	{
		throw new NotImplementedException();
	}
}
