using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Events;
using Blog.ReadModel.Repository;

namespace Blog.ReadModel.Projectors
{
	public class SessionProjector
	{
		public static readonly Guid SubscriptionId = new Guid("4BB3E360-00D9-4A69-AE00-AB28A1C667D7");

		private readonly ISessionRepository Sessions;
		public SessionProjector() : this(Repositories.Sessions) { }
		public SessionProjector(ISessionRepository sessions)
		{
			if (null == sessions)
			{
				throw new ArgumentNullException();
			}

			Sessions = sessions;
		}

		public void Receive(LoggedIn loggedIn)
		{
			Sessions.Save(new Data.Session()
			{
				Id = loggedIn.SessionId,
				UserId = loggedIn.Id
			});
		}
	}
}
