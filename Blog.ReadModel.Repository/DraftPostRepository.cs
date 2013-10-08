using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface IDraftPostRepository : IRepository<DraftPost> {}
	internal class DraftPostRepository : IDraftPostRepository
	{
		private readonly CloudTable DraftPostsTable;
		public DraftPostRepository(CloudTableClient cloudTableClient)
		{
			if (null == cloudTableClient)
			{
				throw new ArgumentNullException();
			}

			DraftPostsTable = cloudTableClient.GetTableReference("DraftPosts");
			DraftPostsTable.CreateIfNotExists();
		}

		public void Save(DraftPost item)
		{
			var entity = CreateEntity(item.Id);
			entity.Properties["WhenCreated"] = new EntityProperty(item.WhenCreated.ToUniversalTime().Ticks);
			entity.Properties["Content"] = new EntityProperty(item.Content ?? "");
			entity.Properties["WhenEdited"] = new EntityProperty(item.WhenEdited.ToUniversalTime().Ticks);
			entity.Properties["Title"] = new EntityProperty(item.Title);
			entity.Properties["IsArticle"] = new EntityProperty(item.IsArticle);
			entity.Properties["ArticleOrder"] = new EntityProperty(item.ArticleOrder);

			DraftPostsTable.Execute(TableOperation.InsertOrMerge(entity));
		}

		public DraftPost Get(Guid id)
		{
			return DraftPostsTable
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreateDraftPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<DraftPost> Get()
		{
			return DraftPostsTable.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateDraftPost(entity));
		}

		public void Delete(Guid id)
		{
			var entity = CreateEntity(id);
			entity.ETag = "*";
			DraftPostsTable.Execute(TableOperation.Delete(entity));
		}

		private DynamicTableEntity CreateEntity(Guid id)
		{
			return new DynamicTableEntity(id.ToString(), "");
		}

		private DraftPost CreateDraftPost(DynamicTableEntity entity)
		{
			return new DraftPost()
			{
				Id = new Guid(entity.PartitionKey),
				WhenCreated = new DateTime(entity.Properties["WhenCreated"].Int64Value.Value).ToLocalTime(),
				Content = entity.Properties["Content"].StringValue,
				WhenEdited = new DateTime(entity.Properties["WhenEdited"].Int64Value.Value).ToLocalTime(),
				Title = entity.Properties["Title"].StringValue,
				IsArticle = entity.Properties.ContainsKey("IsArticle") ? entity.Properties["IsArticle"].BooleanValue.Value : false,
				ArticleOrder = entity.Properties.ContainsKey("ArticleOrder") ? entity.Properties["ArticleOrder"].Int32Value.Value : 0,
			};
		}
	}
}
