using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Blog.ReadModel.Repository
{
	public static class Azure
	{
		public static string ConnectionString { get { return "UseDevelopmentStorage=true"; } }

		public static CloudTable GetTableReference(string tableName)
		{
			var _storageAccount = CloudStorageAccount.Parse(ConnectionString);
			var _tableClient = _storageAccount.CreateCloudTableClient();
			var _table = _tableClient.GetTableReference(tableName);
			_table.CreateIfNotExists();
			return _table;
		}
	}
}
