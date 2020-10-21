using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace TestApp.Mocking
{

    public interface IShippingCostCalculator
    {
        decimal CalculateShippingCost(Order order);
    }

    public class StandardShippingCostCalculator : IShippingCostCalculator
    {
        public static decimal LimitFreeShippingCost => 1000;

        public decimal CalculateShippingCost(Order order)
        {
            return Calculate(order.Total);
        }

        private decimal Calculate(decimal total)
        {
            if (total >= LimitFreeShippingCost)
                return 0;
            else
                return 9.99m;
        }
    }

    public interface IMessageService
    {
        void Send(string message);
    }

    public interface ISmtpClient
    {
        void Send(MailMessage mailMessage);
    }

    public class StandardSmtpClient : ISmtpClient
    {
        private SmtpClient client;

        public void Send(MailMessage mailMessage)
        {
            client.Send(mailMessage);
        }
    }


    public class EmailMessageService : IMessageService
    {
        private readonly ISmtpClient client;

        public EmailMessageService(ISmtpClient client)
        {
            this.client = client;
        }

        public void Send(string message)
        {
            SendEmail(message);
        }

        private void SendEmail(string content)
        {
            if (content == null)
                throw new FormatException();

            MailMessage mailMessage = new MailMessage();
            mailMessage.Body = content;

            client.Send(mailMessage);
        }
    }

    public class OrderSender
    {
        private readonly IShippingCostCalculator shippingCostCalculator;
        private readonly IMessageService messageService;

        public OrderSender(IShippingCostCalculator shippingCostCalculator, IMessageService messageService)
        {
            this.shippingCostCalculator = shippingCostCalculator;
            this.messageService = messageService;
        }

        public void Send(Order order)
        {
            decimal shippingCost = shippingCostCalculator.CalculateShippingCost(order);

            if (shippingCost > 0)
            {
                messageService.Send($"Koszt dostawy {shippingCost}");
            }
        }

      

       
    }

}
