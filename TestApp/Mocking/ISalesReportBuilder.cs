using System.Collections.Generic;

namespace TestApp.Mocking
{


    public interface ISalesReportBuilder
    {
        SalesReport Create();

        void Add(IEnumerable<Order> orders);
    }





}