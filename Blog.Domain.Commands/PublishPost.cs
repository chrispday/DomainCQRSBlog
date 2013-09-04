using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class PublishPost
	{
		public Guid Id { get; set; }
		public DateTime WhenPublished { get; set; }
	}
}
