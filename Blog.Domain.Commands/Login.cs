using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class Login
	{
		private Guid _id = new Guid("6E18A108-1026-4E3D-AB83-C7295B350D31");
		public Guid Id { get { return _id; } }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
