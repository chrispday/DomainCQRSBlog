using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Domain.Events
{
	public class PasswordChanged
	{
		public Guid Id { get; set; }
		public byte[] NewPassword { get; set; }
	}
}
