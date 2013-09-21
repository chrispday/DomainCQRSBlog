using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Commands;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using Blog.ReadModel.Repository;
using DomainCQRS;

namespace Blog.Web.UI
{
	public static class YeastConfig
	{
		public static void Register()
		{
			Config = Configure.With()
			.Synchrounous()
			.DebugLogger()
			.AzureEventStoreProvider(ConfigurationManager.ConnectionStrings["Azure"].ConnectionString)
			.JsonSerializer()
			.EventStore()
			.NoAggregateRootCache()
			.MessageReceiver("Id")
				.Register<CreatePost, Post>()
				.Register<EditPost, Post>()
				.Register<PublishPost, Post>()
				.Register<CreateUser, User>()
				.Register<Login, User>()
				.Register<ChangePassword, User>()
			.EventPublisher()
				.Subscribe<DraftPostProjector, PostCreated>(DraftPostProjector.SubscriptionId)
				.Subscribe<DraftPostProjector, PostEdited>(DraftPostProjector.SubscriptionId)
				.Subscribe<PublishedPostProjector, PostPublished>(PublishedPostProjector.SubscriptionId)
				.Subscribe<UserProjector, UserCreated>(UserProjector.SubscriptionId)
				.Subscribe<UserProjector, PasswordChanged>(UserProjector.SubscriptionId)
				.Subscribe<SessionProjector, LoggedIn>(SessionProjector.SubscriptionId)
				;
			MessageReceiver = Config.GetMessageReceiver;

			if (null == Repositories.Users.Get("admin"))
			{
				MessageReceiver.Receive(new CreateUser() { Id = Guid.NewGuid(), Username = "admin", Password = "admin", Salt = Guid.NewGuid() });
			}
		}

		private static IConfigure Config;
		public static IMessageReceiver MessageReceiver;
	}
}