using System;
using System.Linq;
using StoryQ;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Domain.Commands;
using Blog.Tests;
using Blog.ReadModel.Repository;
using Blog.ReadModel.Data;
using System.Collections.Generic;

[TestClass]
public class Articles_
{
	[TestMethod]
	public void Articles()
	{
		new Story("Articles")
			 .InOrderTo("have pages of content")
			 .AsA("Blogger")
			 .IWant("to create articles")

						.WithScenario("Wrtie Articles")
							 .Given(Nothing)
							 .When(AnArticleIsCreatedAndEdited)
							 .Then(ItEndsUpADraft)
								  .And(IsAnArticle)
								  .And(HasAnOrder)

						.WithScenario("Articles Are Not Posts")
							 .Given(Nothing)
							 .When(AnArticleIsCreatedEditedAndPublished)
							 .Then(ItEndsUpAnArticle)
								  .And(NotAPost)

						.WithScenario("Articles Summaries Are In A Set Order")
							 .Given(Nothing)
							 .When(SomeArticlesArePublishedOutOfOrder)
							 .Then(TheSummariesShouldBeRetrievedInOrder)

						.WithScenario("Article Summaries")
							 .Given(APublishedArticle)
							 .When(TheSummariesAreRetrieved)
							 .Then(TheTitleAndUrlShouldBeAvilable)
								 .And(TheContentIsEmpty)
			 .Execute();
	}

	Guid draftId = Guid.NewGuid();
	private void AnArticleIsCreatedAndEdited()
	{
		_.Receive(new CreatePost() { IsArticle = true, Id = draftId, Title = "Article" + draftId.ToString(), WhenCreated = DateTime.Now, SessionId = _.SessionId });
		_.Receive(new EditPost() { ArticleOrder = 10, Id = draftId, WhenEdited = DateTime.Now, Content = "Content" + draftId.ToString(), SessionId = _.SessionId });
	}

	DraftPost draft;
	private void ItEndsUpADraft()
	{
		draft = Repositories.DraftPosts.Get(draftId);
		Assert.IsNotNull(draft);
	}

	private void IsAnArticle()
	{
		Assert.IsTrue(draft.IsArticle);
	}

	private void HasAnOrder()
	{
		Assert.AreEqual(10, draft.ArticleOrder);
	}

	private void Nothing()
	{
	}

	Guid articleId = Guid.NewGuid();
	private void AnArticleIsCreatedEditedAndPublished()
	{
		PublishArticle(articleId, 0);
	}

	private void ItEndsUpAnArticle()
	{
		Assert.IsTrue(Repositories.PublishedPosts.Get().Any(a => a.Id == articleId && a.IsArticle));
	}

	private void NotAPost()
	{
		Assert.IsFalse(Repositories.PublishedPosts.GetPagedPostsByMostRecentComments(1, int.MaxValue, false).Any(p => p.Id == articleId));
		Assert.IsFalse(Repositories.PublishedPosts.GetPagedPostsByMostRecentP(1, int.MaxValue, false).Any(p => p.Id == articleId));
	}

	private void SomeArticlesArePublishedOutOfOrder()
	{
		PublishArticle(Guid.NewGuid(), 5);
		PublishArticle(Guid.NewGuid(), 3);
		PublishArticle(Guid.NewGuid(), 4);
		PublishArticle(Guid.NewGuid(), 2);
	}

	private void PublishArticle(Guid articleId, int order)
	{
		_.Receive(new CreatePost() { IsArticle = true, Id = articleId, Title = "Article" + articleId.ToString(), WhenCreated = DateTime.Now, SessionId = _.SessionId });
		_.Receive(new EditPost() { ArticleOrder = order, Id = articleId, WhenEdited = DateTime.Now, Content = "Content" + articleId.ToString(), SessionId = _.SessionId });
		_.Receive(new PublishPost() { Id = articleId, WhenPublished = new DateTime(2000, 1, 1), SessionId = _.SessionId });
	}

	private void TheSummariesShouldBeRetrievedInOrder()
	{
		var articles = Repositories.PublishedPosts.GetArticleSummaries();
		var order = articles.First().ArticleOrder;
		foreach (var article in articles.Skip(1))
		{
			Assert.IsTrue(order <= article.ArticleOrder);
			order = article.ArticleOrder;
		}
	}

	private void APublishedArticle()
	{
		PublishArticle(Guid.NewGuid(), 0);
	}

	IEnumerable<PublishedPost> articles;
	private void TheSummariesAreRetrieved()
	{
		articles = Repositories.PublishedPosts.GetArticleSummaries();
	}

	private void TheTitleAndUrlShouldBeAvilable()
	{
		foreach (var article in articles)
		{
			Assert.IsFalse(string.IsNullOrWhiteSpace(article.Title));
			Assert.IsFalse(string.IsNullOrWhiteSpace(article.Url));
		}
	}

	private void TheContentIsEmpty()
	{
		foreach (var article in articles)
		{
			Assert.IsTrue(string.IsNullOrWhiteSpace(article.Content));
		}
	}
}
