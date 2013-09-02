using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Events;
using Blog.ReadModel.Repository;

namespace Blog.ReadModel.Projectors
{
	public class DraftPostProjector
	{
		public static readonly Guid SubscriptionId = new Guid("94DE00B6-2BFF-4066-AC1D-0BE5434A8657");

		public void Receive(PostCreated postCreated)
		{
			var draftPost = Repositories.DraftPosts.Get(postCreated.Id) ?? new Data.DraftPost() { Id = postCreated.Id };
			draftPost.WhenCreated = postCreated.WhenCreated;
			draftPost.WhenEdited = postCreated.WhenCreated;
			draftPost.Title = postCreated.Title;
			Repositories.DraftPosts.Save(draftPost);
		}

		public void Receive(PostEdited postEdited)
		{
			var draftPost = Repositories.DraftPosts.Get(postEdited.Id) ?? new Data.DraftPost { Id = postEdited.Id };
			draftPost.Content = postEdited.Content;
			draftPost.WhenEdited = postEdited.WhenEdited;
			draftPost.Title = postEdited.Title;
			Repositories.DraftPosts.Save(draftPost);
		}
	}
}
