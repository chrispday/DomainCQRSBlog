using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Events;
using Blog.ReadModel.Repository;

namespace Blog.ReadModel.Projectors
{
	public class UserProjector
	{
		public static readonly Guid SubscriptionId = new Guid("AAFDF246-76E7-419E-8BB1-08A6DCE7BE4B");

		private readonly IUserRepository Users;
		public UserProjector() : this(Repositories.Users) { }
		public UserProjector(IUserRepository users)
		{
			if (null == users)
			{
				throw new ArgumentNullException();
			}

			Users = users;
		}

		public void Receive(UserCreated userCreated)
		{
			Users.Save(new Data.User()
			{
				Id = userCreated.Id,
				Username = userCreated.Username,
				Salt = userCreated.Salt,
				Password = userCreated.Password
			});
		}

		public void Receive(PasswordChanged passwordChanged)
		{
			var user = Users.Get(passwordChanged.Id);
			user.Password = passwordChanged.NewPassword;
			Users.Save(user);
		}
	}
}
