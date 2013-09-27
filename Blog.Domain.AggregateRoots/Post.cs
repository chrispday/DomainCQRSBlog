using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.ReadModel.Repository;

namespace Blog.Domain.AggregateRoots
{
	public class Post : AggregateRootBase
	{
		private Guid Id { get; set; }
		private DateTime WhenCreated { get; set; }
		private string Content { get; set; }
		private DateTime WhenEdited { get; set; }
		private string Title { get; set; }
		private DateTime WhenPublished { get; set; }
		private List<Events.CommentAddedToPost> Comments = new List<Events.CommentAddedToPost>();

		public Post(ISessionRepository sessions) : base(sessions) { }
		public Post() : this(Repositories.Sessions) { }

		public object Apply(Commands.CreatePost createPost)
		{
			ValidateSession(createPost.SessionId);
			if (string.IsNullOrWhiteSpace(createPost.Title))
			{
				throw new Errors.PostMustHaveTitleError();
			}

			return new Events.PostCreated()
			{
				Id = createPost.Id,
				WhenCreated = createPost.WhenCreated,
				Title = createPost.Title,
				SessionId = createPost.SessionId
			};
		}

		public void Apply(Events.PostCreated postCreated)
		{
			Id = postCreated.Id;
			WhenCreated = postCreated.WhenCreated;
			Title = postCreated.Title;
		}

		public object Apply(Commands.EditPost editPost)
		{
			ValidateSession(editPost.SessionId);
			if (null == Title
				&& string.IsNullOrWhiteSpace(editPost.Title))
			{
				throw new Errors.PostMustHaveTitleError();
			}

			return new Events.PostEdited()
			{
				Id = editPost.Id,
				Content = editPost.Content ?? Content,
				WhenEdited = editPost.WhenEdited,
				Title = editPost.Title ?? Title,
				SessionId = editPost.SessionId
			};
		}

		public void Apply(Events.PostEdited postEdited)
		{
			Id = postEdited.Id;
			Content = postEdited.Content;
			WhenEdited = postEdited.WhenEdited;
			Title = postEdited.Title;
		}

		public object Apply(Commands.PublishPost publishPost)
		{
			ValidateSession(publishPost.SessionId);
			if (string.IsNullOrWhiteSpace(Content))
			{
				throw new Errors.PostMustHaveContentError();
			}

			return new Events.PostPublished()
			{
				Id = publishPost.Id,
				WhenPublished = publishPost.WhenPublished,
				Title = Title,
				Content = Content,
				SessionId = publishPost.SessionId
			};
		}

		public void Apply(Events.PostPublished postPublished)
		{
			Id = postPublished.Id;
			WhenPublished = postPublished.WhenPublished;
		}

		public object Apply(Commands.AddCommentToPost addCommentToPost)
		{
			if (string.IsNullOrWhiteSpace(addCommentToPost.Name))
			{
				throw new Errors.NameIsEmptyError();
			}
			if (string.IsNullOrWhiteSpace(addCommentToPost.Email))
			{
				throw new Errors.EmailIsEmptyError();
			}
			if (string.IsNullOrWhiteSpace(addCommentToPost.Comment))
			{
				throw new Errors.CommentIsEmptyError();
			}

			return new Events.CommentAddedToPost()
			{
				Id = addCommentToPost.Id,
				CommentId = addCommentToPost.CommentId,
				Name = addCommentToPost.Name,
				Email = addCommentToPost.Email,
				Comment = addCommentToPost.Comment,
				WhenCommented = addCommentToPost.WhenCommented,
				ShowEmail = addCommentToPost.ShowEmail,
				TotalComments = Comments.Count + 1
			};
		}

		public void Apply(Events.CommentAddedToPost commentAddedToPost)
		{
			Comments.Add(commentAddedToPost);
		}
	}
}
