using Microsoft.EntityFrameworkCore;

namespace TestApp.Mocking
{
    public class SalesContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }





}