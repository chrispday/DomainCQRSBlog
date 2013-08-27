using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReadPostsOnASubject_
{
	[TestMethod]
	public void ReadPostsOnASubject()
	{
		new Story("Read Posts on a Subject")
			 .InOrderTo("read other posts on the same subject")
			 .AsA("Reader")
			 .IWant("to a way to see other posts on the same subject")

						.WithScenario("Posts for Subject")
							 .Given(SomePublishedPostsWithSubjects)
							 .When(TheReaderRequestsPostsForASubject)
							 .Then(PostsWithThatSubjectAreShown)
								  .And(TheyAreMostRecentFirst)
								  .And(TheyArePaginated)
			 .Execute();
	}

	private void SomePublishedPostsWithSubjects()
	{
		throw new NotImplementedException();
	}

	private void TheReaderRequestsPostsForASubject()
	{
		throw new NotImplementedException();
	}

	private void PostsWithThatSubjectAreShown()
	{
		throw new NotImplementedException();
	}

	private void TheyAreMostRecentFirst()
	{
		throw new NotImplementedException();
	}

	private void TheyArePaginated()
	{
		throw new NotImplementedException();
	}
}
