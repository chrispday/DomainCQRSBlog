using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public static class Azure
	{
		static string ConnectionString { get { return "UseDevelopmentStorage=true"; } }
		static CloudStorageAccount _storageAccount;
		static CloudStorageAccount StorageAccount { get { if (null == _storageAccount) _storageAccount = CloudStorageAccount.Parse(ConnectionString); return _storageAccount; } }
		static CloudTableClient _tableClient;
		public static CloudTableClient TableClient { get { if (null == _tableClient) _tableClient = StorageAccount.CreateCloudTableClient(); return _tableClient; } }
	}
}
