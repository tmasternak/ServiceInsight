﻿namespace ServiceInsight.Tests
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using ServiceInsight.Explorer.EndpointExplorer;
    using ServiceInsight.Framework;
    using ServiceInsight.Framework.Events;
    using ServiceInsight.MessageList;
    using ServiceInsight.MessageProperties;
    using ServiceInsight.Models;
    using ServiceInsight.Search;
    using ServiceInsight.ServiceControl;
    using Shouldly;

    [TestFixture]
    public class MessageListViewModelTests
    {
        IRxEventAggregator eventAggregator;
        IWorkNotifier workNotifier;
        IServiceControl serviceControl;
        SearchBarViewModel searchBar;
        Func<MessageListViewModel> messageListFunc;
        IClipboard clipboard;

        [SetUp]
        public void TestInitialize()
        {
            eventAggregator = Substitute.For<IRxEventAggregator>();
            workNotifier = Substitute.For<IWorkNotifier>();
            serviceControl = Substitute.For<IServiceControl>();
            searchBar = Substitute.For<SearchBarViewModel>();
            clipboard = Substitute.For<IClipboard>();
            messageListFunc = () => new MessageListViewModel(eventAggregator,
                                                   workNotifier,
                                                   serviceControl,
                                                   searchBar,
                                                   Substitute.For<GeneralHeaderViewModel>(),
                                                   Substitute.For<MessageSelectionContext>(),
                                                   clipboard);
        }

        [Test]
        [Ignore("Needs Pirac testing to set schedulers")]
        public void Should_load_the_messages_from_the_endpoint()
        {
            var endpoint = new Endpoint { Host = "localhost", Name = "Service" };
            serviceControl.GetAuditMessages(Arg.Is(endpoint), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                             .Returns(x => new PagedResult<StoredMessage>
                             {
                                 CurrentPage = 1,
                                 TotalCount = 100,
                                 Result = new List<StoredMessage>
                                 {
                                     new StoredMessage(),
                                     new StoredMessage()
                                 }
                             });

            var messageList = messageListFunc();

            messageList.Handle(new SelectedExplorerItemChanged(new AuditEndpointExplorerItem(endpoint)));

            workNotifier.Received(1).NotifyOfWork(Arg.Any<string>());
            messageList.Rows.Count.ShouldBe(2);
            searchBar.IsVisible.ShouldBe(true);
        }
    }
}