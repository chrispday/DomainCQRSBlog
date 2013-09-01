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
	}
}
