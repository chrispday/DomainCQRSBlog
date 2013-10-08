using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface IPublishedPostRepository : IRepository<PublishedPost>
	{
		PublishedPost GetByUrl(string url);
		IEnumerable<PublishedPost> GetPagedPostsByMostRecentP(int page, int pageSize, bool withOneMore);
		IEnumerable<PublishedPost> GetPagedPostsByMostRecentComments(int page, int pageSize, bool withOneMore);
		IEnumerable<PublishedPost> GetArticleSummaries();
		void SaveForRecentComment(PublishedPost item);
	}

	internal class PublishedPostRepository : IPublishedPostRepository
	{
		private readonly CloudTable PublishedPostsByDatePublishedDesc;
		private readonly CloudTable PublishedPostsByUrl;
		private readonly CloudTable PublishedPostsByMostRecentComment;
		private readonly CloudTable ArticleSummariesByOrder;
		public PublishedPostRepository(CloudTableClient cloudTableClient)
		{
			if (null == cloudTableClient)
			{
				throw new ArgumentNullException();
			}

			PublishedPostsByDatePublishedDesc = cloudTableClient.GetTableReference("PublishedPostsByDatePublishedDesc");
			PublishedPostsByDatePublishedDesc.CreateIfNotExists();
			PublishedPostsByUrl = cloudTableClient.GetTableReference("PublishedPostsByUrl");
			PublishedPostsByUrl.CreateIfNotExists();
			PublishedPostsByMostRecentComment = cloudTableClient.GetTableReference("PublishedPostsByMostRecentComment");
			PublishedPostsByMostRecentComment.CreateIfNotExists();
			ArticleSummariesByOrder = cloudTableClient.GetTableReference("ArticleSummariesByOrder");
			ArticleSummariesByOrder.CreateIfNotExists();
		}

		public void Save(PublishedPost item)
		{
			SaveByUrl(item);

			if (!item.IsArticle)
			{
				SavePublishedPostsByDatePublishedDesc(item);
			}
			else
			{
				SaveArticleSummaryByOrder(item);
			}
		}

		private void SaveArticleSummaryByOrder(PublishedPost item)
		{
			var order = item.ArticleOrder.ToString("D12") + item.Id.ToString();
			var entity = new DynamicTableEntity(order, item.Id.ToString());
			entity.Properties["Title"] = new EntityProperty(item.Title);
			entity.Properties["Url"] = new EntityProperty(item.Url);
			entity.Properties["ArticleOrder"] = new EntityProperty(item.ArticleOrder);

			ArticleSummariesByOrder.Execute(TableOperation.InsertOrMerge(entity));
		}

		private void SaveByUrl(PublishedPost item)
		{
			var entity = new DynamicTableEntity(item.Url, item.Id.ToString());
			SavePublishedPostProperties(item, entity);
			PublishedPostsByUrl.Execute(TableOperation.InsertOrMerge(entity));
		}

		private void SavePublishedPostsByDatePublishedDesc(PublishedPost item)
		{
			var order = (DateTime.MaxValue.ToUniversalTime() - item.WhenPublished.ToUniversalTime()).Ticks.ToString("D12");
			var entity = new DynamicTableEntity(order + item.Id.ToString(), item.Id.ToString());
			SavePublishedPostProperties(item, entity);
			PublishedPostsByDatePublishedDesc.Execute(TableOperation.InsertOrMerge(entity));
		}

		private void SavePublishedPostProperties(PublishedPost item, DynamicTableEntity entity)
		{
			entity.Properties["WhenPublished"] = new EntityProperty(item.WhenPublished.ToUniversalTime().Ticks);
			entity.Properties["Url"] = new EntityProperty(item.Url ?? "");
			entity.Properties["Content"] = new EntityProperty(item.Content ?? "");
			entity.Properties["Title"] = new EntityProperty(item.Title ?? "");
			entity.Properties["TotalComments"] = new EntityProperty(item.TotalComments);
			entity.Properties["MostRecentCommentBy"] = new EntityProperty(item.MostRecentCommentBy);
			entity.Properties["MostRecentCommentWhen"] = new EntityProperty(item.MostRecentCommentWhen.ToUniversalTime().Ticks);
			entity.Properties["IsArticle"] = new EntityProperty(item.IsArticle);
			entity.Properties["ArticleOrder"] = new EntityProperty(item.ArticleOrder);
		}

		public void SaveForRecentComment(PublishedPost item)
		{
			Save(item);

			if (!item.IsArticle)
			{
				var order = (DateTime.MaxValue.ToUniversalTime() - item.MostRecentCommentWhen.ToUniversalTime()).Ticks.ToString("D12");
				var entity = new DynamicTableEntity(order + Guid.NewGuid().ToString(), item.Id.ToString());
				entity.Properties["Title"] = new EntityProperty(item.Title ?? "");
				entity.Properties["Url"] = new EntityProperty(item.Url);
				entity.Properties["TotalComments"] = new EntityProperty(item.TotalComments);
				entity.Properties["MostRecentCommentBy"] = new EntityProperty(item.MostRecentCommentBy);
				entity.Properties["MostRecentCommentWhen"] = new EntityProperty(item.MostRecentCommentWhen.ToUniversalTime().Ticks);

				PublishedPostsByMostRecentComment.Execute(TableOperation.Insert(entity));
			}
		}

		public IEnumerable<PublishedPost> GetArticleSummaries()
		{
			return ArticleSummariesByOrder.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateArticleSummary(entity));
		}

		private PublishedPost CreateArticleSummary(DynamicTableEntity entity)
		{
			var articleSummary = new PublishedPost();
			articleSummary.Url = entity.Properties["Url"].StringValue;
			articleSummary.Title = entity.Properties["Title"].StringValue;
			articleSummary.ArticleOrder = entity.Properties["ArticleOrder"].Int32Value.Value;
			return articleSummary;
		}

		public PublishedPost GetByUrl(string url)
		{
			return PublishedPostsByUrl
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, url)))
				.Select(entity => CreatePublishedPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<PublishedPost> Get()
		{
			return PublishedPostsByUrl.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreatePublishedPost(entity));
		}

		public PublishedPost Get(Guid id)
		{
			return PublishedPostsByUrl
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreatePublishedPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<PublishedPost> GetPagedPostsByMostRecentP(int page, int pageSize, bool withOneMore)
		{
			return PublishedPostsByDatePublishedDesc.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Skip((page - 1) * pageSize)
				.Take(pageSize + (withOneMore ? 1 : 0))
				.Select(entity => CreatePublishedPost(entity));
		}

		public IEnumerable<PublishedPost> GetPagedPostsByMostRecentComments(int page, int pageSize, bool withOneMore)
		{
			return PublishedPostsByMostRecentComment.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Skip((page - 1) * pageSize)
				.Take(pageSize + (withOneMore ? 1 : 0))
				.Select(entity => CreatePublishedPost(entity));
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		private PublishedPost CreatePublishedPost(DynamicTableEntity entity)
		{
			var publishedPost = new PublishedPost();
			publishedPost.Id = new Guid(entity.RowKey);
			publishedPost.WhenPublished = entity.Properties.ContainsKey("WhenPublished") ? new DateTime(entity.Properties["WhenPublished"].Int64Value.Value).ToLocalTime() : DateTime.MinValue;
			publishedPost.Url = entity.Properties.ContainsKey("Url") ? entity.Properties["Url"].StringValue : null;
			publishedPost.Content = entity.Properties.ContainsKey("Content") ? entity.Properties["Content"].StringValue : null;
			publishedPost.Title = entity.Properties["Title"].StringValue;
			publishedPost.TotalComments = entity.Properties.ContainsKey("TotalComments") ? entity.Properties["TotalComments"].Int32Value.Value : 0;
			publishedPost.MostRecentCommentBy = entity.Properties.ContainsKey("MostRecentCommentBy") ? entity.Properties["MostRecentCommentBy"].StringValue : null;
			publishedPost.MostRecentCommentWhen = entity.Properties.ContainsKey("MostRecentCommentWhen") ? new DateTime(entity.Properties["MostRecentCommentWhen"].Int64Value.Value).ToLocalTime() : DateTime.MinValue;
			publishedPost.IsArticle = entity.Properties.ContainsKey("IsArticle") ? entity.Properties["IsArticle"].BooleanValue.Value : false;
			publishedPost.ArticleOrder = entity.Properties.ContainsKey("ArticleOrder") ? entity.Properties["ArticleOrder"].Int32Value.Value : 0;

			return publishedPost;
		}
	}
}
