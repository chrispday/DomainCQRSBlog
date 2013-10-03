using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Data
{
	public class Comment
	{
		public Guid Id { get; set; }
		public Guid PostId { get; set; }
		public string Name { get; set; }
		public string EmailHash { get; set; }
		public string CommentText { get; set; }
		public DateTime WhenCommented { get; set; }
		public string Homepage { get; set; }
	}
}
