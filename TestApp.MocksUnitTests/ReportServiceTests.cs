using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{

    // dotnet add package MockQueryable.Moq
    public class UserServiceTests
    {
        [Fact]
        public void GetBossesRecipients_WhenHasBosses_ReturnsUsers()
        {
            // Arrange

            var users = new List<Mocking.User>
            {
                new Employee { IsBoss = true, Email = "a"}
            };

            Mock<DbSet<Mocking.User>> mockUsers = users.AsQueryable().BuildMockDbSet();

            Mock<SalesContext> mockSalesContext = new Mock<SalesContext>();

            mockSalesContext
                .Setup(sc => sc.Users)
                .Returns(mockUsers.Object);

            IUserService userService = new UserService(mockSalesContext.Object);

            // Act
            var result = userService.GetBossesRecipients();

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void GetBossesRecipients_WhenHasNotBosses_ReturnsEmpty()
        {
            // Arrange

            var users = new List<Mocking.User>
            {
                new Employee { IsBoss = false, Email = "a"}
            };

            Mock<DbSet<Mocking.User>> mockUsers = users.AsQueryable().BuildMockDbSet();

            Mock<SalesContext> mockSalesContext = new Mock<SalesContext>();

            mockSalesContext
                .Setup(sc => sc.Users)
                .Returns(mockUsers.Object);

            IUserService userService = new UserService(mockSalesContext.Object);

            // Act
            var result = userService.GetBossesRecipients();

            // Assert
            result.Should().BeEmpty();
        }
    }

    public class SalesReportBuilderTests
    {
        [Fact]
        public void Create_OrdersIsEmpty_ShouldThrowsApplicationException()
        {
            // Arrange
            ISalesReportBuilder salesReportBuilder = new SalesReportBuilder();
            salesReportBuilder.Add(new List<Order>());

            // Act
            Action act = () => salesReportBuilder.Create();

            // Assert
            act.Should().ThrowExactly<ApplicationException>();

        }

        [Fact]
        public void Create_OrdersIsNotEmpty_ShouldReturnSalesReport()
        {
            // Arrange
            ISalesReportBuilder salesReportBuilder = new SalesReportBuilder();
            salesReportBuilder.Add(new List<Order>() { new Order() });

            // Act
            var result = salesReportBuilder.Create();

            // Assert
            result.Should().NotBeNull();

        }

    }


    public class ReportServiceTests
    {
        [Fact] 
        public async Task SendSalesReportEmailAsync_OrdersIsEmpty_ShouldNotSendSalesReport()
        {
            // Arrange
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();

            mockOrderService
                .Setup(os => os.Get(It.IsAny<OrderSearchCriteria>()))
                .Returns(new Collection<Order>());

            Mock<IUserService> mockUserService = new Mock<IUserService>();

            Mock<ISalesReportBuilder> mockSalesReportBuilder = new Mock<ISalesReportBuilder>();

            IOrderService orderService = mockOrderService.Object;
            IUserService userService = mockUserService.Object;
            ISalesReportBuilder salesReportBuilder = mockSalesReportBuilder.Object;

            Mock<IMessageClient> mockMessageClient = new Mock<IMessageClient>();

            mockMessageClient
                .Setup(mc => mc.SendAsync(It.IsAny<TestApp.Mocking.User>(), It.IsAny<TestApp.Mocking.User>(), It.IsAny<SalesReport>())).Verifiable();


            IMessageClient messageClient = mockMessageClient.Object;

            ReportService reportService = new ReportService(orderService, userService, salesReportBuilder, messageClient);

            // Act
            await reportService.SendSalesReportEmailAsync(DateTime.Now);

            // Assert
            mockMessageClient.Verify(mc => mc.SendAsync(It.IsAny<Mocking.User>(), It.IsAny<Mocking.User>(), It.IsAny<SalesReport>()), Times.Never);


        }

        [Fact]
        public async void SendSalesReportEmailAsync_OrdersIsNotEmpty_ShouldSendSalesReport()
        {
            // Arrange
            Mock<IOrderService> mockOrderService = new Mock<IOrderService>();

            mockOrderService
                .Setup(os => os.Get(It.IsAny<OrderSearchCriteria>()))
                .Returns(new Collection<Order> { new Order { OrderedDate = DateTime.Now.AddDays(-3) } });

            Mock<IUserService> mockUserService = new Mock<IUserService>();

            mockUserService.Setup(us => us.GetBossesRecipients())
                .Returns(new List<Mocking.User>() { new Mocking.Employee() });

            mockUserService.Setup(us => us.GetBot())
               .Returns(new Mocking.Bot());

            Mock<ISalesReportBuilder> mockSalesReportBuilder = new Mock<ISalesReportBuilder>();

            IOrderService orderService = mockOrderService.Object;
            IUserService userService = mockUserService.Object;
            ISalesReportBuilder salesReportBuilder = mockSalesReportBuilder.Object;

            Mock<IMessageClient> mockMessageClient = new Mock<IMessageClient>();

            mockMessageClient
                .Setup(mc => mc.SendAsync(It.IsAny<TestApp.Mocking.User>(), It.IsAny<TestApp.Mocking.User>(), It.IsAny<SalesReport>())).Verifiable();


            IMessageClient messageClient = mockMessageClient.Object;

            ReportService reportService = new ReportService(orderService, userService, salesReportBuilder, messageClient);

            // Act
            await reportService.SendSalesReportEmailAsync(DateTime.Now);

            // Assert
            mockMessageClient.Verify(mc => mc.SendAsync(It.IsAny<Mocking.User>(), It.IsAny<Mocking.User>(), It.IsAny<SalesReport>()), Times.AtLeast(1));
        }

       
    }
}
