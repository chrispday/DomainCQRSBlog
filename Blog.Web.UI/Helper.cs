using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.UI
{
	public static class Helper
	{
		public static string TwoLetterSuffix(this DateTime @this)
		{
			var dayMod10 = @this.Day % 10;

			if (dayMod10 > 3 || dayMod10 == 0 || (@this.Day >= 10 && @this.Day <= 19))
			{
				return "\\t\\h";
			}
			else if (dayMod10 == 1)
			{
				return "\\s\\t";
			}
			else if (dayMod10 == 2)
			{
				return "n\\d";
			}
			else
			{
				return "r\\d";
			}
		}
	}
}