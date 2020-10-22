using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    #region Models

    public class Order
    {
        public DateTime OrderedDate { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
        }

        public List<OrderDetail> Details { get; set; }

        public virtual decimal Total => Details.Sum(d => d.Total);
    }

    public enum OrderStatus
    {
        Boxing,
        Sent,
    }

    public class OrderDetail
    {
        public OrderDetail(decimal unitPrice, short quantity = 1)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }

    public abstract class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }


    public class Employee : User
    {
        public bool IsBoss { get; set; }
    }

    public class Bot : User
    {

    }

    public abstract class Report
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; }

        public Report()
        {
            CreatedOn = DateTime.Now;
        }
    }

    public class SalesReport : Report
    {
        public TimeSpan TotalTime { get; set; }

        public decimal TotalAmount { get; set; }


        public override string ToString()
        {
            return $"Report created on {CreatedOn} \r\n TotalAmount: {TotalAmount}";
        }

        public string ToHtml()
        {
            return $"<html>Report created on <b>{CreatedOn}</b> <p>TotalAmount: {TotalAmount}<p></html>";
        }
    }

#endregion

    public class ReportService
    {
        public delegate void ReportSentHandler(object sender, ReportSentEventArgs e);
        public event ReportSentHandler ReportSent;

        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly ISalesReportBuilder salesReportBuilder;
        private readonly IMessageClient client;

        public ReportService(
            IOrderService orderService,
            IUserService userService,
            ISalesReportBuilder salesReportBuilder,
            IMessageClient client)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.salesReportBuilder = salesReportBuilder;
            this.client = client;
        }

        public async Task SendSalesReportEmailAsync(DateTime date)
        {
            var criteria = new OrderSearchCriteria(date.AddDays(-7), date);

            var orders = orderService.Get(criteria);

            if (!orders.Any())
            {
                return;
            }

            salesReportBuilder.Add(orders);

            SalesReport report = salesReportBuilder.Create();

            IEnumerable<User> recipients = userService.GetBossesRecipients();

            var sender = userService.GetBot();

            foreach (var recipient in recipients)
            {
                await client.SendAsync(sender, recipient, report);
            }
        }
    }





}