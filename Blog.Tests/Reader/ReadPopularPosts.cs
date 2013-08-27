using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReadPopularPosts_
{
	[TestMethod]
	public void ReadPopularPosts()
	{
		new Story("Read Popular Posts")
			 .InOrderTo("read Posts that other people have read the most.")
			 .AsA("Reader")
			 .IWant("to see a list of Posts that have been read most often.")

						.WithScenario("Popular Posts")
							 .Given(SomePublishedPosts)
								  .And(EachOfThePostsHasBeenReadADifferentNumberOfTimes)
							 .When(TheMostPopularPostsAreRequested)
							 .Then(TheReaderWillSeePostsWithTheMostReadFirst)
								  .And(TheyArePaginated)
			 .Execute();
	}

	private void SomePublishedPosts()
	{
		throw new NotImplementedException();
	}

	private void EachOfThePostsHasBeenReadADifferentNumberOfTimes()
	{
		throw new NotImplementedException();
	}

	private void TheMostPopularPostsAreRequested()
	{
		throw new NotImplementedException();
	}

	private void TheReaderWillSeePostsWithTheMostReadFirst()
	{
		throw new NotImplementedException();
	}

	private void TheyArePaginated()
	{
		throw new NotImplementedException();
	}
}
