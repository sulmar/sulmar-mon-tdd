using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{
    public class OrderSenderLinqToMocksTests
    {
        [Fact]
        public void Send_ShippingCostAboveZero_ShouldSendMessage()
        {
            //IMessageService messageService = Mock.Of<IMessageService>(ms => ms.Send(It.IsAny<string>());




            //// Assert
            //Mock<IMessageService> mockMessageService = Mock.Get(messageService);

            //mockMessageService.Verify(ms => ms.Send(It.IsAny<string>()), Times.Once);


                
        }
    }
}
