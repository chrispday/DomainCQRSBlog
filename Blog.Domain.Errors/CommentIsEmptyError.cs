﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Errors
{
	public class CommentIsEmptyError : Exception
	{
		public override string Message
		{
			get
			{
				return "Comment is empty";
			}
		}
	}
}
