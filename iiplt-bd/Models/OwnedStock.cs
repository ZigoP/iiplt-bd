namespace iiplt_bd.Models
{
    public class OwnedStock
    {
        public required Stock Stock { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
