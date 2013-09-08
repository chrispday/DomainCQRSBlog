using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class ChangePassword
	{
		public Guid Id { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
	}
}
