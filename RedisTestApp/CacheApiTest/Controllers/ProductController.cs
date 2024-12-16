using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using CacheApiTest.Data;
using Microsoft.EntityFrameworkCore;
using CacheApiTest.Models;

namespace CacheApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly RedisTestAppContext _context;

        public ProductController(RedisTestAppContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [HttpGet("none-cached",Name = "GetNoneCachedProducts")]
        public async Task<IEnumerable<Product>> GetNoneCachedProducts()
        {
            IEnumerable<Product> products = await _context.Product.ToListAsync();

            return products;
        }

        [HttpGet("cached", Name = "GetCachedProducts")]
        public async Task<IEnumerable<Product>> GetCachedProducts()
        {
            const string cacheKey = "ProductList";
            string serializedProducts;
            IEnumerable<Product> products;

            // get  data from  cache
            var cachedProducts = await _cache.GetStringAsync(cacheKey);

            if (cachedProducts != null)
            {
                serializedProducts = cachedProducts;
                products = JsonSerializer.Deserialize<IEnumerable<Product>>(serializedProducts);
            }
            else
            {
                products = await _context.Product.ToListAsync();

                // Serialize and cache the data
                serializedProducts = JsonSerializer.Serialize(products);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)) // Cache expiration - will expire no matter what
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Sliding expiration - will expire after set time if not used
                await _cache.SetStringAsync(cacheKey, serializedProducts, options);
            }

            return products;
        }
    }
}
