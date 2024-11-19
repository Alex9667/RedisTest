using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedisTestApp.Models;

namespace RedisTestApp.Data
{
    public class RedisTestAppContext : DbContext
    {
        public RedisTestAppContext (DbContextOptions<RedisTestAppContext> options)
            : base(options)
        {
        }

        public DbSet<RedisTestApp.Models.Product> Product { get; set; } = default!;
    }
}
