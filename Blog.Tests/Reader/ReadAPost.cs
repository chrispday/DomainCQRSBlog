using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ReadAPost_
{
	[TestMethod]
	public void ReadAPost()
	{
		new Story("Read a Post")
			 .InOrderTo("read a Post.")
			 .AsA("Reader")
			 .IWant("to see a post")

						.WithScenario("Read a Post")
							 .Given(APublishedPost)
							 .When(APostIsAskedFor)
							 .Then(TheReaderCanReadThePost)
			 .Execute();
	}

	private void APublishedPost()
	{
		throw new NotImplementedException();
	}

	private void APostIsAskedFor()
	{
		throw new NotImplementedException();
	}

	private void TheReaderCanReadThePost()
	{
		throw new NotImplementedException();
	}
}
