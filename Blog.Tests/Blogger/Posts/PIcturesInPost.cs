using System;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PicturesInPosts_
{
	[TestMethod]
	public void PicturesInPosts()
	{
		new Story("Pictures in Posts")
			 .InOrderTo("include Pictures in posts.")
			 .AsA("Blogger")
			 .IWant("to upload pictures")
				  .And("add a link to the Picture in the Post.")

						.WithScenario("Upload Picture")
							 .Given(APictureHasBeenUploaded)
							 .When(ALinkToAPictureIsAdded)
							 .Then(ALinkToWhereThePictureWasUploadedIsInTheListOfPictures)
			 .Execute();
	}

	private void APictureHasBeenUploaded()
	{
		throw new NotImplementedException();
	}

	private void ALinkToAPictureIsAdded()
	{
		throw new NotImplementedException();
	}

	private void ALinkToWhereThePictureWasUploadedIsInTheListOfPictures()
	{
		throw new NotImplementedException();
	}
}
