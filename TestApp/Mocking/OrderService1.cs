using System.Collections.Generic;
using System.Linq;

namespace TestApp.Mocking
{
    public class OrderService : IOrderService
    {
        private readonly SalesContext context;

        public OrderService(SalesContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> Get(OrderSearchCriteria criteria)
        {
            IQueryable<Order> orders = context.Orders;

            if (criteria.From.HasValue)
            {
                orders = orders.Where(o => o.OrderedDate > criteria.From);
            }

            if (criteria.To.HasValue)
            {
                orders = orders.Where(o => o.OrderedDate <= criteria.To);
            }

            return orders;
        }

        

    }





}