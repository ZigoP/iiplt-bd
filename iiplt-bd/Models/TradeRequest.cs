using System.ComponentModel;

namespace iiplt_bd.Models
{
    public enum TradeType
    {
        [Description("BUY")]
        BUY = 1,
        [Description("SELL")]
        SELL = 2
    }

    public class TradeRequest
    {
        public required string ISIN { get; set; }
        public int Quantity { get; set; }
        public TradeType Type { get; set; }
    }
}
