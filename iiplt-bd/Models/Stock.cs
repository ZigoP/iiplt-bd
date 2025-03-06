namespace iiplt_bd.Models
{
    public class Stock
    {
        public string Name { get; set; }
        public string ISIN { get; set; }
        public decimal Price { get; set; }

        public Stock(string name, string isin, decimal price)
        {
            Name = name;
            ISIN = isin;
            Price = price;
        }
    }
}
