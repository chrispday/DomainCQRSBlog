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
			var entity = new DynamicTableEntity(item.Id.ToString(), "");
			entity.Properties["UserId"] = new EntityProperty(item.UserId);
			Sessions.Execute(TableOperation.InsertOrMerge(entity));
		}

		public Session Get(Guid id)
		{
			return Sessions
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString())))
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
				Id = new Guid(entity.PartitionKey),
				UserId = entity.Properties["UserId"].GuidValue.Value
			};
		}
	}
}
