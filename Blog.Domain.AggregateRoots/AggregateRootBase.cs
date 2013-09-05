using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.ReadModel.Repository;

namespace Blog.Domain.AggregateRoots
{
	public class AggregateRootBase
	{
		protected void ValidateSession(Guid sessionId)
		{
			var session = Repositories.Sessions.Get(sessionId);
			if (null == session)
			{
				throw new Errors.NotLoggedInError();
			}
		}
	}
}
