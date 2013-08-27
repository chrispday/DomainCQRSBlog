using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class Advertising_
{
	[TestMethod]
	public void Advertising()
	{
		new Story("Advertising")
			 .InOrderTo("recoup the cost of hosting.")
			 .AsA("Blogger")
			 .IWant("to host advertising.")

						.WithScenario("Display Advertising")
							 .Given(Nothing)
							 .When(AReaderIsViewingPosts)
							 .Then(AdvertisingIsShown)
			 .Execute();
	}

	private void Nothing()
	{
		throw new NotImplementedException();
	}

	private void AReaderIsViewingPosts()
	{
		throw new NotImplementedException();
	}

	private void AdvertisingIsShown()
	{
		throw new NotImplementedException();
	}
}
