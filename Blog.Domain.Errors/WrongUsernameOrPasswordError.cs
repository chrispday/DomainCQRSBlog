using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Errors
{
	public class WrongUsernameOrPasswordError : Exception
	{
		public override string Message
		{
			get
			{
				return "Wrong username or password";
			}
		}
	}
}
