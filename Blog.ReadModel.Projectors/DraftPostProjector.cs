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
		private IDraftPostRepository _repository = new DraftPostRepository();

		public void Receive(PostCreated postCreated)
		{
			var draftPost = _repository.Get(postCreated.AggregateRootId);

			if (null == draftPost)
			{
				draftPost = new Data.DraftPost() { Id = postCreated.AggregateRootId, WhenCreated = postCreated.WhenCreated };
				_repository.Save(draftPost);
			}
		}
	}
}
