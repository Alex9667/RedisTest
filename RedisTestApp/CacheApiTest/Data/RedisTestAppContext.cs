using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CacheApiTest.Models;

namespace CacheApiTest.Data
{
    public class RedisTestAppContext : DbContext
    {
        public RedisTestAppContext (DbContextOptions<RedisTestAppContext> options)
            : base(options)
        {
        }

        public DbSet<CacheApiTest.Models.Product> Product { get; set; } = default!;
    }
}
