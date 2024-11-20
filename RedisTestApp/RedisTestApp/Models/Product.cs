namespace RedisTestApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }

        public Product(double price, string color)
        {
            Price = price;
            Color = color;
        }
    }
}
