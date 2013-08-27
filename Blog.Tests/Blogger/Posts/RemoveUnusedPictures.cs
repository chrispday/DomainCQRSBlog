using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RemoveUneededPicture_
{
	[TestMethod]
	public void RemoveUneededPicture()
	{
		new Story("Remove Uneeded Picture")
			 .InOrderTo("remove Pictures that aren't needed")
			 .AsA("Blogger")
			 .IWant("to delete Pictures")

						.WithScenario("Remove Picture")
							 .Given(APictureHasBeenUploaded)
							 .When(ThePictureIsRemoved)
							 .Then(ItShouldNotAppearInTheListOfPicturesAvailable)
			 .Execute();
	}

	private void APictureHasBeenUploaded()
	{
		throw new NotImplementedException();
	}

	private void ThePictureIsRemoved()
	{
		throw new NotImplementedException();
	}

	private void ItShouldNotAppearInTheListOfPicturesAvailable()
	{
		throw new NotImplementedException();
	}
}
