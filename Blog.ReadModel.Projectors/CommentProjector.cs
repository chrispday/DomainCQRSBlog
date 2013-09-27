using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Blog.ReadModel.Repository;
using Blog.Domain.Events;

namespace Blog.ReadModel.Projectors
{
	public class CommentProjector
	{
		public static readonly Guid SubscriptionId = new Guid("2138FD4C-2B2B-4DFE-980B-2A556602C433");

		private readonly ICommentRepository Comments;
		public CommentProjector() : this(Repositories.Comments) { }
		public CommentProjector(ICommentRepository comments)
		{
			if (null == comments)
			{
				throw new ArgumentNullException();
			}

			Comments = comments;
		}

		public void Receive(CommentAddedToPost commentAddedToPost)
		{
			Repositories.Comments.Save(new Data.Comment()
			{
				Id = commentAddedToPost.CommentId,
				PostId = commentAddedToPost.Id,
				Name = commentAddedToPost.Name,
				Email = commentAddedToPost.Email,
				EmailHash = new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes(commentAddedToPost.Email)),
				CommentText = commentAddedToPost.Comment,
				ShowEmail = commentAddedToPost.ShowEmail,
				WhenCommented = commentAddedToPost.WhenCommented
			});
		}
	}
}
