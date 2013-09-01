using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class CreatePost
	{
		public Guid AggregateRootId { get; set; }
		public DateTime WhenCreated { get; set; }
	}
}
