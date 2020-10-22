using System;

namespace TestApp.Mocking
{
    public class OrderSearchCriteria : SearchCriteria
    {
        public OrderSearchCriteria(DateTime? from, DateTime? to)
        {
            From = from;
            To = to;
        }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }





}