using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Tests;
using Blog.Domain.Commands;
using Blog.ReadModel.Repository;
using Blog.ReadModel.Data;

[TestClass]
public class HowAReaderFoundPost_
{
    [TestMethod]
    public void HowAReaderFoundPost()
    {
        new Story("How A Reader Found Post (Refback)")
            .InOrderTo("follow back to other pages to see what has been written about my Post.")
            .AsA("Blogger")
            .IWant("to know how a Reader found a Post.")

                    .WithScenario("track referrer")
                        .Given(APost)
                        .When(AReaderIsReferredFromAnotherPage)
                        .Then(ThatReferrerIsAddedToThePost)
            .Execute();
    }

	 Guid postId = Guid.NewGuid();
	 private void APost()
    {
		 _.Receive(new CreatePost() { Id = postId, SessionId = _.SessionId, Title = postId.ToString(), WhenCreated = DateTime.Now });
		 _.Receive(new EditPost() { Id = postId, SessionId = _.SessionId, Content = postId.ToString(), WhenEdited = DateTime.Now });
		 _.Receive(new PublishPost() { Id = postId, SessionId = _.SessionId, WhenPublished = DateTime.Now });
	 }

    private void AReaderIsReferredFromAnotherPage()
    {
		 Repositories.Referrers.Save(new Referrer() { PostId = postId, ReferrerUrl = postId.ToString(), RequestUrl = _.SessionId.ToString(), WhenReferred = new DateTime(2000, 1, 1) });
    }

    private void ThatReferrerIsAddedToThePost()
    {
		 Assert.IsTrue(Repositories.Referrers.GetForPost(postId).Any(r =>
			 r.PostId == postId
			 && r.ReferrerUrl == postId.ToString()
			 && r.RequestUrl == _.SessionId.ToString()
			 && r.WhenReferred == new DateTime(2000, 1, 1)));
    }
}
