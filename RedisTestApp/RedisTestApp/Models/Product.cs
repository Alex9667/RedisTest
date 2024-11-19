namespace RedisTestApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public required string Color { get; set; }
    }
}
