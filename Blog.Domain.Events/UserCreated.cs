using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Events
{
	public class UserCreated
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public Guid Salt { get; set; }
		public byte[] Password { get; set; }
	}
}
