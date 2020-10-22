using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestApp.Mocking
{

    public class SalesReportBuilder : ISalesReportBuilder
    {
        private IEnumerable<Order> orders;

        public SalesReportBuilder()
        {
            orders = new Collection<Order>();
        }

        public void Add(IEnumerable<Order> orders)
        {
            this.orders = orders;
        }

        public SalesReport Create()
        {
            if (!orders.Any())
                throw new ApplicationException();

            SalesReport salesReport = new SalesReport();

            salesReport.TotalAmount = orders.Sum(o => o.Total);

            return salesReport;
        }


        
    }





}