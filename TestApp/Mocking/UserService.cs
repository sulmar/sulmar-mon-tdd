using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.Mocking
{
    //public interface IOrderRepository
    //{
    //    IQueryable<Order> Get(Func<Order, bool> expression, IQueryable<Order> orders);
    //    IQueryable<Order> Get(OrderSearchCriteria criteria, IQueryable<Order> orders);
    //}

    //public class OrderRepository : IOrderRepository
    //{
    //    public IQueryable<Order> Get(Func<Order, bool> expression, IQueryable<Order> orders)
    //    {
    //        return orders.Where(expression).AsQueryable();
    //    }

    //    public IQueryable<Order> Get(OrderSearchCriteria criteria, IQueryable<Order> orders)
    //    {
    //        if (criteria.From.HasValue)
    //        {
    //            orders = orders.Where(o => o.OrderedDate > criteria.From);
    //        }

    //        if (criteria.To.HasValue)
    //        {
    //            orders = orders.Where(o => o.OrderedDate <= criteria.To);
    //        }

    //        return orders;
    //    }
    //}

    public class UserService : IUserService
    {
        private readonly SalesContext salesContext;

        public UserService(SalesContext salesContext)
        {
            this.salesContext = salesContext;
        }

        public IEnumerable<User> Get(UserSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetBossesRecipients()
        {
            return salesContext.Users.OfType<Employee>().Where(e => e.IsBoss && !string.IsNullOrEmpty(e.Email)).ToList();
        }

        public User GetBot()
        {
            return salesContext.Users.OfType<Bot>().Single();
        }
    }





}