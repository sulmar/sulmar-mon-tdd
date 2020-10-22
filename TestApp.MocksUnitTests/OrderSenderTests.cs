using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Mocking;
using Xunit;
using FluentAssertions;
using System.Net.Mail;

namespace TestApp.MocksUnitTests
{

    public class SmtpClientWrapper : ISmtpClient
    {
        public SmtpClient SmtpClient { get; set; }

        public SmtpClientWrapper(string host, int port)
        {
            SmtpClient = new SmtpClient(host, port);
        }
        public void Send(MailMessage mailMessage)
        {
            SmtpClient.Send(mailMessage);
        }
    }

    public class EmailMessageServiceTests
    {
        private IMessageService messageService;

        private Mock<ISmtpClient> mockSmtpClient;

        public EmailMessageServiceTests()
        {
            mockSmtpClient = new Mock<ISmtpClient>();
            messageService = new EmailMessageService(mockSmtpClient.Object);
        }

        [Fact]
        public void Send_ValidMessage_ShouldSent()
        {
            // Arrange
            mockSmtpClient
                .Setup(sc => sc.Send(It.IsAny<MailMessage>()))
                .Verifiable();

            // Act
            messageService.Send("a");

            // Assert
            // act.Should().NotThrow();

            mockSmtpClient.Verify(sc => sc.Send(It.IsAny<MailMessage>()), Times.Once);
        }

        [Fact]
        public void Send_InvalidMessage_ShouldThrowFormatException()
        {
            // Act
            Action act= ()=> messageService.Send(null);

            // Assert
            act.Should().ThrowExactly<FormatException>();
        }
    }

    public class OrderSenderTests
    {
        private Mock<IMessageService> mockMessageService;
        private IMessageService messageService;
        private OrderSender orderSender;

        private Mock<IShippingCostCalculator> mockShippingCostCalculator;
        private IShippingCostCalculator shippingCostCalculator;

        public OrderSenderTests()
        {
            mockMessageService = new Mock<IMessageService>();
            messageService = mockMessageService.Object;

            mockShippingCostCalculator = new Mock<IShippingCostCalculator>();
            shippingCostCalculator = mockShippingCostCalculator.Object;

            orderSender = new OrderSender(shippingCostCalculator, messageService);
        }


        [Fact]
        public void Send_ShippingCostAboveZero_ShouldSendMessage()
        {
            // Arrange
            mockMessageService
                .Setup(ms => ms.Send(It.IsAny<string>()))
                .Verifiable();

            mockShippingCostCalculator
                .Setup(c => c.CalculateShippingCost(It.IsAny<Order>()))
                .Returns(0.01m);
           
            // Act
            orderSender.Send(new Order());

            // Assert
            mockMessageService.Verify(ms => ms.Send(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public void Send_ShippingCostGratis_ShouldNotSendMessage()
        {
            // Arrange
            mockMessageService
                .Setup(ms => ms.Send(It.IsAny<string>()))
                .Verifiable();

            mockShippingCostCalculator
                .Setup(c => c.CalculateShippingCost(It.IsAny<Order>()))
                .Returns(0m);

            // Act
            orderSender.Send(new Order());

            // Assert
            mockMessageService.Verify(ms => ms.Send(It.IsAny<string>()), Times.Never);
        }


    }
}
