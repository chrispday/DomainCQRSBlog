using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface ISessionRepository : IRepository<Session>
	{
		Session GetByUser(Guid userId);
	}

	public class SessionRepository : ISessionRepository
	{
		private readonly CloudTable Sessions;
		public SessionRepository(CloudTableClient cloudTableClient)
		{
			if (null == cloudTableClient)
			{
				throw new ArgumentNullException();
			}

			Sessions = cloudTableClient.GetTableReference("Sessions");
			Sessions.CreateIfNotExists();
		}

		public void Save(Session item)
		{
			var entity = new DynamicTableEntity(item.UserId.ToString(), "");
			entity.Properties["Id"] = new EntityProperty(item.Id);
			Sessions.Execute(TableOperation.InsertOrMerge(entity));
		}

		public Session Get(Guid id)
		{
			return Get().Where(s => s.Id == id).FirstOrDefault();
		}

		public Session GetByUser(Guid userId)
		{
			return Sessions
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId.ToString())))
				.Select(entity => CreateSession(entity))
				.FirstOrDefault();
		}

		public IEnumerable<Session> Get()
		{
			return Sessions.ExecuteQuery(new TableQuery<DynamicTableEntity>())
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
				Id = entity.Properties["Id"].GuidValue.Value,
				UserId = new Guid(entity.PartitionKey)
			};
		}
	}
}
