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
		protected readonly ISessionRepository Sessions;
		public AggregateRootBase(ISessionRepository sessions)
		{
			if (null == sessions)
			{
				throw new ArgumentNullException();
			}

			Sessions = sessions;
		}

		protected void ValidateSession(Guid sessionId)
		{
			var session = Sessions.Get(sessionId);
			if (null == session)
			{
				throw new Errors.NotLoggedInError();
			}
		}
	}
}
