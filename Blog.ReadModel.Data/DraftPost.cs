using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Data
{
	public class DraftPost
	{
		public Guid Id { get; set; }
		public DateTime WhenCreated { get; set; }
		public string Content { get; set; }
		public DateTime WhenEdited { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public bool IsArticle { get; set; }
		public int ArticleOrder { get; set; }
	}
}
