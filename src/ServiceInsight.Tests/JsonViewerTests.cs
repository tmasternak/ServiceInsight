﻿namespace ServiceInsight.Tests
{
    using MessageViewers;
    using NSubstitute;
    using NUnit.Framework;
    using Pirac;
    using ServiceInsight.MessageViewers.JsonViewer;
    using ServiceInsight.Models;
    using ServiceInsight.ServiceControl;

    [TestFixture]
    public class JsonViewerTests
    {
        JsonMessageViewModel viewModel;
        IMessageViewer view;

        [SetUp]
        public void TestInitialize()
        {
            view = Substitute.For<IMessageViewer>();
            viewModel = new JsonMessageViewModel();
            ((IActivatable)viewModel).Activate();
        }

        [Test]
        public void Should_display_json_message()
        {
            const string TestMessage = @"[{""$type"":""NSB.Messages.CRM.RegisterCustomer, NSB.Messages"",""Name"":""Hadi"",""Password"":""123456"",""EmailAddress"":""h.eskandari@gmail.com"",""RegistrationDate"":""2013-01-28T03:24:05.0546437Z""}]";

            ((IHaveView)viewModel).AttachView(view);

            viewModel.SelectedMessage = new MessageBody { Body = new PresentationBody { Text = TestMessage } };

            view.Received(1).Display(Arg.Any<string>());
        }
    }
}