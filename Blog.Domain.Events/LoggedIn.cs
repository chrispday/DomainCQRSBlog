using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events
{
	public class LoggedIn
	{
		public Guid Id { get; set; }
		public Guid SessionId { get; set; }
	}
}
