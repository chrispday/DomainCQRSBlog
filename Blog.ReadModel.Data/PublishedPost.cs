﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Data
{
	public class PublishedPost
	{
		public Guid Id { get; set; }
		public DateTime WhenPublished { get; set; }
		public string Url { get; set; }
	}
}
