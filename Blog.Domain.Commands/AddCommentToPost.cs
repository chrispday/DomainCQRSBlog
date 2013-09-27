using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class AddCommentToPost
	{
		public Guid Id { get; set; }
		public Guid CommentId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Comment { get; set; }
		public DateTime WhenCommented { get; set; }
		public bool ShowEmail { get; set; }
	}
}
