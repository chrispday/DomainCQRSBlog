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
		IEnumerable<PublishedPost> MostRecentPosts(int page, int pageSize, bool withOneMore);
		PublishedPost GetByUrl(string url);
	}

	internal class PublishedPostRepository : IPublishedPostRepository
	{
		private readonly CloudTable PublishedPostsByDatePublishedDesc;
		private readonly CloudTable PublishedPostsByUrl;
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
		}

		public void Save(PublishedPost item)
		{
			var order = (DateTime.MaxValue.ToUniversalTime() - item.WhenPublished.ToUniversalTime()).Ticks.ToString("D12");
			var entity = new DynamicTableEntity(order + item.Id.ToString(), item.Id.ToString());
			entity.Properties["WhenPublished"] = new EntityProperty(item.WhenPublished.ToUniversalTime().Ticks);
			entity.Properties["Url"] = new EntityProperty(item.Url ?? "");
			entity.Properties["Content"] = new EntityProperty(item.Content ?? "");
			entity.Properties["Title"] = new EntityProperty(item.Title ?? "");

			PublishedPostsByDatePublishedDesc.Execute(TableOperation.InsertOrMerge(entity));

			if (!string.IsNullOrWhiteSpace(item.Url))
			{
				entity = new DynamicTableEntity(item.Url, item.Id.ToString());
				entity.Properties["WhenPublished"] = new EntityProperty(item.WhenPublished.ToUniversalTime().Ticks);
				entity.Properties["Url"] = new EntityProperty(item.Url ?? "");
				entity.Properties["Content"] = new EntityProperty(item.Content ?? "");
				entity.Properties["Title"] = new EntityProperty(item.Title ?? "");
				PublishedPostsByUrl.Execute(TableOperation.InsertOrMerge(entity));
			}
		}

		public PublishedPost Get(Guid id)
		{
			return PublishedPostsByDatePublishedDesc
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreatePublishedPost(entity))
				.FirstOrDefault();
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
			return PublishedPostsByDatePublishedDesc.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreatePublishedPost(entity));
		}

		public IEnumerable<PublishedPost> MostRecentPosts(int page, int pageSize, bool withOneMore)
		{
			return PublishedPostsByDatePublishedDesc.ExecuteQuery(new TableQuery<DynamicTableEntity>())
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
			return new PublishedPost()
			{ 
				Id = new Guid(entity.RowKey), 
				WhenPublished = new DateTime(entity.Properties["WhenPublished"].Int64Value.Value).ToLocalTime(),
				Url = entity.Properties["Url"].StringValue,
				Content = entity.Properties["Content"].StringValue,
				Title = entity.Properties["Title"].StringValue
			};
		}
	}
}
