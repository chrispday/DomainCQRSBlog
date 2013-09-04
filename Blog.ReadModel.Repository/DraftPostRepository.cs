using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface IDraftPostRepository : IRepository<DraftPost>
	{
	}

	internal class DraftPostRepository : IDraftPostRepository
	{
		private CloudTable _table = Azure.GetTableReference("DraftPosts");

		public void Save(DraftPost item)
		{
			var entity = new DynamicTableEntity(item.Id.ToString(), "");
			entity.Properties["WhenCreated"] = new EntityProperty(item.WhenCreated.ToUniversalTime().Ticks);
			entity.Properties["Content"] = new EntityProperty(item.Content ?? "");
			entity.Properties["WhenEdited"] = new EntityProperty(item.WhenEdited.ToUniversalTime().Ticks);
			entity.Properties["Title"] = new EntityProperty(item.Title);

			_table.Execute(TableOperation.InsertOrMerge(entity));
		}

		public DraftPost Get(Guid id)
		{
			return _table
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.CombineFilters(
					TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString()),
					TableOperators.And,
					TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, ""))))
				.Select(entity => CreateDraftPost(entity))
				.FirstOrDefault();
		}

		public IEnumerable<DraftPost> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateDraftPost(entity));
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
				};
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
