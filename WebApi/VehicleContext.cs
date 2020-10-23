using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    public class VehiclesContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
