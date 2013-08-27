using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReadMostRecentPosts_
{
	[TestMethod]
	public void ReadMostRecentPosts()
	{
		new Story("Read Most Recent Posts")
			 .InOrderTo("read the most recent posts first.")
			 .AsA("Reader")
			 .IWant("to see Posts ordered by most recent first.")

						.WithScenario("See Most Recent Posts")
							 .Given(SomePublishedPosts)
							 .When(TheMostRecentPostsAreAskedFor)
							 .Then(TheReaderCanViewPostsWithTheMostRecentFirst)
								  .And(TheyArePaginated)
			 .Execute();
	}

	private void SomePublishedPosts()
	{
		throw new NotImplementedException();
	}

	private void TheMostRecentPostsAreAskedFor()
	{
		throw new NotImplementedException();
	}

	private void TheReaderCanViewPostsWithTheMostRecentFirst()
	{
		throw new NotImplementedException();
	}

	private void TheyArePaginated()
	{
		throw new NotImplementedException();
	}
}
