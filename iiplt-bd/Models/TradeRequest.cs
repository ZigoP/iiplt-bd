namespace iiplt_bd.Models
{
    public enum TradeType
    {
        BUY,
        SELL
    }

    public class TradeRequest
    {
        public required string ISIN { get; set; }
        public int Quantity { get; set; }
        public TradeType Type { get; set; }
    }
}
