using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface IUserRepository : IRepository<User, string> { }
	internal class UserRepository : IUserRepository
	{
		private CloudTable _table = Azure.GetTableReference("Users");
		private CloudTable _tableByUsername = Azure.GetTableReference("UsersByUsername");

		public void Save(User item)
		{
			var entity = new DynamicTableEntity(item.Id.ToString(), "");
			entity.Properties["Username"] = new EntityProperty(item.Username);
			entity.Properties["Salt"] = new EntityProperty(item.Salt);
			entity.Properties["Password"] = new EntityProperty(item.Password);
			_table.Execute(TableOperation.InsertOrMerge(entity));

			entity = new DynamicTableEntity(item.Username, item.Id.ToString());
			_tableByUsername.Execute(TableOperation.InsertOrMerge(entity));
		}

		public User Get(Guid id)
		{
			return _table
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString())))
				.Select(entity => CreateUser(entity))
				.FirstOrDefault();
		}

		public User Get(string id)
		{
			return Get(_tableByUsername
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id)))
				.Select(entity => new Guid(entity.RowKey))
				.FirstOrDefault());
		}

		public IEnumerable<User> Get()
		{
			return _table.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateUser(entity));
		}

		private User CreateUser(DynamicTableEntity entity)
		{
			return new User()
			{
				Id = new Guid(entity.PartitionKey),
				Username = entity.Properties["Username"].StringValue,
				Salt = entity.Properties["Salt"].GuidValue.Value,
				Password	= entity.Properties["Password"].BinaryValue
			};
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
