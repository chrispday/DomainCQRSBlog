using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events
{
	public class PostCreated
	{
		public Guid Id { get; set; }
		public DateTime WhenCreated { get; set; }
		public string Title { get; set; }
	}
}
