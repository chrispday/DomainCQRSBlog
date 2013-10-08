using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events
{
	public class PostEdited
	{
		public Guid Id { get; set; }
		public string Content { get; set; }
		public DateTime WhenEdited { get; set; }
		public string Title { get; set; }
		public Guid SessionId { get; set; }
		public int ArticleOrder { get; set; }
	}
}
