using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class CreateUser
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public Guid Salt { get; set; }
		public string Password { get; set; }
	}
}
