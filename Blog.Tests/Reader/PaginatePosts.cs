using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PaginatePosts_
{
	[TestMethod]
	public void PaginatePosts()
	{
		new Story("Paginate Posts")
			 .InOrderTo("not display too many Posts on one page")
			 .AsA("Reader")
			 .IWant("to see Posts split over pages")

						.WithScenario("Paginate Posts")
							 .Given(SomePublishedPosts)
							 .When(ASetOfPostsAreRequested)
								  .And(ThereAreMorePostsThanThePageSize)
							 .Then(TheReaderCanViewThePostsOnePageAtATime)
			 .Execute();
	}

	private void SomePublishedPosts()
	{
		throw new NotImplementedException();
	}

	private void ASetOfPostsAreRequested()
	{
		throw new NotImplementedException();
	}

	private void ThereAreMorePostsThanThePageSize()
	{
		throw new NotImplementedException();
	}

	private void TheReaderCanViewThePostsOnePageAtATime()
	{
		throw new NotImplementedException();
	}
}
