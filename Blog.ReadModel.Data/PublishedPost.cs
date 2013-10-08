using System;
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
		public string Content { get; set; }
		public string Title { get; set; }
		public int TotalComments { get; set; }
		public string MostRecentCommentBy { get; set; }
		public DateTime MostRecentCommentWhen { get; set; }
		public bool IsArticle { get; set; }
		public int ArticleOrder { get; set; }
	}
}
