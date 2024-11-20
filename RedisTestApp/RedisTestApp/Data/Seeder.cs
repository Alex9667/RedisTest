using RedisTestApp.Models;

namespace RedisTestApp.Data
{
    public class Seeder
    {
        private readonly RedisTestAppContext _context;

        public Seeder(RedisTestAppContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (!_context.Product.Any())
            {
                for (int j = 0; j < 100; j++)
                {
                    Product prod = new(Convert.ToDouble(j), "Yellow");
                    _context.Add(prod);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
