﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class AddCommentToPost
	{
		public Guid Id { get; set; }
		public Guid CommentId { get; set; }
		public string Name { get; set; }
		public string EmailHash { get; set; }
		public string Comment { get; set; }
		public DateTime WhenCommented { get; set; }
		public string Homepage { get; set; }
	}
}
