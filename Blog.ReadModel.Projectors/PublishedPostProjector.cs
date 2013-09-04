using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.Domain.Events;
using Blog.ReadModel.Data;
using Blog.ReadModel.Repository;

namespace Blog.ReadModel.Projectors
{
	public class PublishedPostProjector
	{
		public static readonly Guid SubscriptionId = new Guid("59FBFA16-DD60-4CF0-920E-63B07BE9CE65");

		public void Receive(PostPublished postPublished)
		{
			var publishedPost = Repositories.PublishedPosts.Get(postPublished.Id) ?? new PublishedPost() { Id = postPublished.Id };
			publishedPost.WhenPublished = postPublished.WhenPublished;
			publishedPost.Url = CreateUrl(postPublished.Title);
			Repositories.PublishedPosts.Save(publishedPost);
		}

		private static string CreateUrl(string title)
		{
			var existingUrls = Repositories.PublishedPosts.Get().Select(p => p.Url);
			var url = new string(title.Select(c => Char.IsLetterOrDigit(c) ? c : '-').ToArray());

			while (existingUrls.Contains(url))
			{
				url += "-";
			}

			return url;
		}
	}
}
