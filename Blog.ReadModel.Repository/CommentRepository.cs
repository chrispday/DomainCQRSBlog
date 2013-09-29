using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface ICommentRepository : IRepository<Comment>
	{
		IEnumerable<Comment> GetForPost(Guid postId);
	}

	internal class CommentRepository : ICommentRepository
	{
		private readonly CloudTable CommentsByWhenTable;
		public CommentRepository(CloudTableClient cloudTableClient)
		{
			if (null == cloudTableClient)
			{
				throw new ArgumentNullException();
			}

			CommentsByWhenTable = cloudTableClient.GetTableReference("CommentsByWhen");
			CommentsByWhenTable.CreateIfNotExists();
		}

		public void Save(Comment item)
		{
			var order = item.WhenCommented.ToUniversalTime().Ticks.ToString("D12");
			var entity = new DynamicTableEntity(item.PostId.ToString(), order + item.Id.ToString());
			entity.Properties["Id"] = new EntityProperty(item.Id);
			entity.Properties["Name"] = new EntityProperty(item.Name);
			entity.Properties["Email"] = new EntityProperty(item.Email);
			entity.Properties["EmailHash"] = new EntityProperty(item.EmailHash);
			entity.Properties["Comment"] = new EntityProperty(item.CommentText);
			entity.Properties["WhenCommented"] = new EntityProperty(item.WhenCommented.ToUniversalTime().Ticks);
			entity.Properties["ShowEmail"] = new EntityProperty(item.ShowEmail);

			CommentsByWhenTable.Execute(TableOperation.InsertOrMerge(entity));
		}

		public Comment Get(Guid id)
		{
			return Get().Where(c => c.Id == id).FirstOrDefault();
		}

		public IEnumerable<Comment> Get()
		{
			return CommentsByWhenTable.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateComment(entity));
		}

		public IEnumerable<Comment> GetForPost(Guid postId)
		{
			return CommentsByWhenTable
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, postId.ToString())))
				.Select(entity => CreateComment(entity));
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		private Comment CreateComment(DynamicTableEntity entity)
		{
			return new Comment()
			{
				Id = entity.Properties["Id"].GuidValue.Value,
				PostId = new Guid(entity.PartitionKey),
				Name = entity.Properties["Name"].StringValue,
				Email = entity.Properties["Email"].StringValue,
				EmailHash = entity.Properties["EmailHash"].StringValue,
				CommentText = entity.Properties["Comment"].StringValue,
				WhenCommented = new DateTime(entity.Properties["WhenCommented"].Int64Value.Value).ToLocalTime(),
				ShowEmail = entity.Properties["ShowEmail"].BooleanValue.Value
			};
		}
	}
}
