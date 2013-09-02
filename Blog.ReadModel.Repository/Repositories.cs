using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Repository
{
	public static class Repositories
	{
		public static readonly IDraftPostRepository DraftPosts = new DraftPostRepository();
		public static readonly IPublishedPostRepository PublishedPosts = new PublishedPostRepository();
	}
}
