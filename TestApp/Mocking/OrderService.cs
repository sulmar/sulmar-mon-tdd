using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Mocking
{
    public class OrderSender
    {
        public void Send(Order order)
        {
            decimal shippingCost = CalculateShippingCost(order);

            SendEmail($"Koszt dostawy {shippingCost}");
        }

        private void SendEmail(string messsage)
        {
            Console.WriteLine($"Sending {messsage}...");
        }

        private decimal CalculateShippingCost(Order order)
        {
            if (order.Total > 1000)
                return 0;
            else
                return 9.99m;
        }
    }

}
