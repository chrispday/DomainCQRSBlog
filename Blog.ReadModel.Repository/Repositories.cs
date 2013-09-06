using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadModel.Repository
{
	public static class Repositories
	{
		public static readonly IDraftPostRepository DraftPosts = new DraftPostRepository(Azure.TableClient);
		public static readonly IPublishedPostRepository PublishedPosts = new PublishedPostRepository(Azure.TableClient);
		public static readonly IUserRepository Users = new UserRepository(Azure.TableClient);
		public static readonly ISessionRepository Sessions = new SessionRepository(Azure.TableClient);
	}
}
