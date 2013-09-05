using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Data
{
	public class User
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public Guid Salt { get; set; }
		public byte[] Password { get; set; }
	}
}
