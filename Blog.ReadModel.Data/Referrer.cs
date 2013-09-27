using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Data
{
	public class Referrer
	{
		public Guid PostId { get; set; }
		public string ReferrerUrl { get; set; }
		public string RequestUrl { get; set; }
		public DateTime WhenReferred { get; set; }
	}
}
