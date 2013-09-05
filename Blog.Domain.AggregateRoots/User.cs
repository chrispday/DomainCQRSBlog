﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain;
using Blog.Domain.Errors;
using Blog.ReadModel.Data;
using Blog.ReadModel.Repository;

namespace Blog.Domain.AggregateRoots
{
	public class User
	{
		private Guid Id { get; set; }
		private List<Guid> Sessions = new List<Guid>();
		public string Username { get; set; }
		public Guid Salt { get; set; }
		public byte[] Password { get; set; }

		public object Apply(Commands.CreateUser createUser)
		{
			if (string.IsNullOrWhiteSpace(createUser.Username))
			{
				throw new UserMustHaveAUsernameError();
			}

			if (null != Repositories.Users.Get(createUser.Username))
			{
				throw new UserAlreadyExistsError();
			}

			return new Events.UserCreated()
			{
				Id = createUser.Id,
				Username = createUser.Username,
				Salt = createUser.Salt,
				Password = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(createUser.Password + createUser.Salt.ToString()))
			};
		}

		public void Apply(Events.UserCreated userCreated)
		{
			Id = userCreated.Id;
			Username = userCreated.Username;
			Salt = userCreated.Salt;
			Password = userCreated.Password;
		}

		public IEnumerable<object> Apply(Commands.Login login)
		{
			var user = Repositories.Users.Get(login.Username);
			if (null == user)
			{
				throw new WrongUsernameOrPasswordError();
			}
			var password = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(login.Password + user.Salt.ToString()));
			if (!password.SequenceEqual(user.Password))
			{
				throw new WrongUsernameOrPasswordError();
			}

			Repositories.Sessions.Save(new Session() { Id = Guid.NewGuid(), UserId = user.Id });

			return Enumerable.Empty<object>();
		}
	}
}
