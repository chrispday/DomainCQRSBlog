﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Commands
{
	public class CreatePost
	{
		public Guid Id { get; set; }
		public DateTime WhenCreated { get; set; }
		public string Title { get; set; }
		public Guid SessionId { get; set; }
		public bool IsArticle { get; set; }
	}
}
