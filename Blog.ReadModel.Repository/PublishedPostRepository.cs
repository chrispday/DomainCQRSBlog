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
	}

	internal class PublishedPostRepository : IPublishedPostRepository
	{
		private CloudTable _table = Azure.GetTableReference("PublishedPosts");

		public void Save(PublishedPost item)
		{
			var entity = new DynamicTableEntity(item.Id.ToString(), "");
			entity.Properties["WhenPublished"] = new EntityProperty(item.WhenPublished.ToUniversalTime().Ticks);
			entity.Properties["Url"] = new EntityProperty(item.Url ?? "");

			_table.Execute(TableOperation.InsertOrMerge(entity));
		}

		public PublishedPost Get(Guid id)
		{
			return _table
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.CombineFilters(
					TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString()),
					TableOperators.And,
					TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, ""))))
				.Select(entity => CreatePublishedPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<PublishedPost> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
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
				Id = new Guid(entity.PartitionKey), 
				WhenPublished = new DateTime(entity.Properties["WhenPublished"].Int64Value.Value).ToLocalTime(),
				Url = entity.Properties["Url"].StringValue
			};
		}
	}
}
