using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.AggregateRoots;
using Blog.Domain.Commands;
using Blog.Domain.Events;
using Blog.ReadModel.Projectors;
using Yeast.EventStore;

namespace Blog.Tests
{
	public static class _
	{
		public static IConfigure Config = Configure.With()
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
			.EventPublisher()
				.Subscribe<DraftPostProjector, PostCreated>(DraftPostProjector.SubscriptionId)
				.Subscribe<DraftPostProjector, PostEdited>(DraftPostProjector.SubscriptionId)
				.Subscribe<PublishedPostProjector, PostPublished>(PublishedPostProjector.SubscriptionId)
				;
	}
}
