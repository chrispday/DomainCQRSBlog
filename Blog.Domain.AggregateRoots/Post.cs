using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;

namespace Blog.Domain.AggregateRoots
{
	public class Post
	{
		private Guid AggregateRootId { get; set; }
		private bool Published { get; set; }
		private DateTime WhenCreated { get; set; }

		public object Apply(Commands.CreatePost createPost)
		{
			AggregateRootId = createPost.AggregateRootId;
			Published = false;

			return new Events.PostCreated() { AggregateRootId = createPost.AggregateRootId, WhenCreated = createPost.WhenCreated };
		}

		public void Apply(Events.PostCreated postCreated)
		{
			AggregateRootId = postCreated.AggregateRootId;
			WhenCreated = postCreated.WhenCreated;
		}
	}
}
