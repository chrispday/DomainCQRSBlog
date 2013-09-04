using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events
{
	public class PostPublished
	{
		public Guid Id { get; set; }
		public DateTime WhenPublished { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}
