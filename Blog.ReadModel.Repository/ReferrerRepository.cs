using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.ReadModel.Data;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public interface IReferrerRepository : IRepository<Referrer>
	{
		IEnumerable<Referrer> GetForPost(Guid postId);
	}

	internal class ReferrerRepository : IReferrerRepository
	{
		private readonly CloudTable ReferrerByWhenDescTable;
		public ReferrerRepository(CloudTableClient cloudTableClient)
		{
			if (null == cloudTableClient)
			{
				throw new ArgumentNullException();
			}

			ReferrerByWhenDescTable = cloudTableClient.GetTableReference("ReferrerByWhen");
			ReferrerByWhenDescTable.CreateIfNotExists();
		}

		public void Save(Referrer item)
		{
			var order = (DateTime.MaxValue - item.WhenReferred.ToUniversalTime()).Ticks.ToString("D12");
			var entity = new DynamicTableEntity(item.PostId.ToString(), order);
			entity.Properties["ReferrerUrl"] = new EntityProperty(item.ReferrerUrl);
			entity.Properties["RequestUrl"] = new EntityProperty(item.RequestUrl);
			entity.Properties["WhenReferred"] = new EntityProperty(item.WhenReferred.ToUniversalTime().Ticks);

			ReferrerByWhenDescTable.Execute(TableOperation.Insert(entity));
		}

		public Referrer Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Referrer> Get()
		{
			return ReferrerByWhenDescTable.ExecuteQuery(new TableQuery<DynamicTableEntity>())
				.Select(entity => CreateReferrer(entity));
		}

		public IEnumerable<Referrer> GetForPost(Guid postId)
		{
			return ReferrerByWhenDescTable
				.ExecuteQuery(new TableQuery<DynamicTableEntity>()
				.Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, postId.ToString())))
				.Select(entity => CreateReferrer(entity));
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		private Referrer CreateReferrer(DynamicTableEntity entity)
		{
			return new Referrer()
			{
				PostId = new Guid(entity.PartitionKey),
				ReferrerUrl = entity.Properties["ReferrerUrl"].StringValue,
				RequestUrl = entity.Properties["RequestUrl"].StringValue,
				WhenReferred = new DateTime(entity.Properties["WhenReferred"].Int64Value.Value).ToLocalTime()
			};
		}
	}
}
