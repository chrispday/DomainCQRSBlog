﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Commands;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using Blog.ReadModel.Repository;
using Yeast.EventStore;

namespace Blog.Web.UI
{
	public static class YeastConfig
	{
		public static void Register()
		{
			Config = Configure.With()
			.Synchrounous()
			.DebugLogger()
			.AzureEventStoreProvider("UseDevelopmentStorage=true")
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
				;
			MessageReceiver = Config.GetMessageReceiver;

			if (null == Repositories.Users.Get("admin"))
			{
				MessageReceiver.Receive(new CreateUser() { Id = Guid.NewGuid(), Username = "admin", Password = "admin", Salt = Guid.NewGuid() });
			}
		}

		private static IConfigure Config;
		public static IMessageReceiver MessageReceiver;

		public static Guid SessionId(HttpCookieCollection cookies)
		{
			var sessionId = default(Guid);
			var sessionCookie = cookies["SessionId"];
			if (null != sessionCookie)
			{
				sessionId = new Guid(sessionCookie.Value);
			}
			return sessionId;
		}

		public static Blog.ReadModel.Data.Session Session(Guid sessionId)
		{
			return Repositories.Sessions.Get(sessionId);
		}
	}
}