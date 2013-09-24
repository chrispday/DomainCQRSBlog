using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Commands;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using Blog.ReadModel.Repository;
using DomainCQRS;

namespace Blog.Tests
{
	public static class _
	{
		public static IBuiltConfigure Config = Configure.With()
			.Synchrounous()
			.DebugLogger()
			.AzureEventStoreProvider("UseDevelopmentStorage=true")
			.JsonSerializer()
			.EventStore()
			.NoAggregateRootCache()
			.MessageReceiver("Id")
			.EventPublisher()
			.Build()
				.Register<CreatePost, Post>()
				.Register<EditPost, Post>()
				.Register<PublishPost, Post>()
				.Register<CreateUser, User>()
				.Register<Login, User>()
				.Register<ChangePassword, User>()
				.Subscribe<DraftPostProjector, PostCreated>(DraftPostProjector.SubscriptionId)
				.Subscribe<DraftPostProjector, PostEdited>(DraftPostProjector.SubscriptionId)
				.Subscribe<PublishedPostProjector, PostPublished>(PublishedPostProjector.SubscriptionId)
				.Subscribe<UserProjector, UserCreated>(UserProjector.SubscriptionId)
				.Subscribe<UserProjector, PasswordChanged>(UserProjector.SubscriptionId)
				.Subscribe<SessionProjector, LoggedIn>(SessionProjector.SubscriptionId)
				;

		private static Guid? _sessionId;
		public static Guid SessionId
		{
			get
			{
				if (null == _sessionId)
				{
					var session = Repositories.Sessions.Get().FirstOrDefault();
					if (null == session)
					{
						var id = Guid.NewGuid();
						_.Receive(new CreateUser() { Id = id, Username = id.ToString(), Salt = id, Password = id.ToString() });
						_.Receive(new Login() { Username = id.ToString(), Password = id.ToString() });
						session = Repositories.Sessions.Get().First();
					}
					_sessionId = session.Id;
				}

				return _sessionId.Value;
			}
		}

		public static void Receive(object command)
		{
			Config.MessageReceiver.Receive(command);
		}
	}
}
