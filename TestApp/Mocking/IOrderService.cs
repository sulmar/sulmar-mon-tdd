using System.Collections.Generic;

namespace TestApp.Mocking
{
    public interface IOrderService
    {
        IEnumerable<Order> Get(OrderSearchCriteria criteria);
    }





}