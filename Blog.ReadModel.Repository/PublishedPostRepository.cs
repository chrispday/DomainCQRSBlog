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
	}

	internal class PublishedPostRepository : IPublishedPostRepository
	{
		private CloudTable _table = Azure.GetTableReference("PublishedPosts");

		public void Save(PublishedPost item)
		{
			var order = (DateTime.MaxValue.ToUniversalTime() - item.WhenPublished.ToUniversalTime()).Ticks.ToString("D12");
			var entity = new DynamicTableEntity(order + item.Id.ToString(), item.Id.ToString());
			entity.Properties["WhenPublished"] = new EntityProperty(item.WhenPublished.ToUniversalTime().Ticks);
			entity.Properties["Url"] = new EntityProperty(item.Url ?? "");
			entity.Properties["Content"] = new EntityProperty(item.Content ?? "");

			_table.Execute(TableOperation.InsertOrMerge(entity));
		}

		public PublishedPost Get(Guid id)
		{
			return _table
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreatePublishedPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<PublishedPost> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreatePublishedPost(entity));
		}

		public IEnumerable<PublishedPost> MostRecentPosts(int page, int pageSize, bool withOneMore)
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
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
				Content = entity.Properties["Content"].StringValue
			};
		}
	}
}
