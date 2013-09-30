﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Errors
{
	public class UserAlreadyExistsError : Exception
	{
		public override string Message
		{
			get
			{
				return "User already exists";
			}
		}
	}
}
