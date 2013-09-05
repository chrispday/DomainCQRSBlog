using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface ISessionRepository : IRepository<Session> { }
	public class SessionRepository : ISessionRepository
	{
		private CloudTable _table = Azure.GetTableReference("Sessions");

		public void Save(Session item)
		{
			var entity = new DynamicTableEntity(item.Id.ToString(), "");
			entity.Properties["UserId"] = new EntityProperty(item.UserId);
			_table.Execute(TableOperation.InsertOrMerge(entity));
		}

		public Session Get(Guid id)
		{
			return _table
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreateSession(entity))
				.FirstOrDefault();
		}

		public IEnumerable<Session> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateSession(entity));
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		private Session CreateSession(DynamicTableEntity entity)
		{
			return new Session()
			{
				Id = new Guid(entity.PartitionKey),
				UserId = entity.Properties["UserId"].GuidValue.Value
			};
		}
	}
}
