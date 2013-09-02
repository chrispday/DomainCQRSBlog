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

			_table.Execute(TableOperation.InsertOrMerge(entity));
		}

		public PublishedPost Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PublishedPost> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => new PublishedPost() { Id = new Guid(entity.PartitionKey) });
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
