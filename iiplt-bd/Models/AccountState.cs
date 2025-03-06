using System.Collections.Generic;

namespace iiplt_bd.Models
{
    public class AccountState
    {
        public decimal Balance { get; set; } = 10000;
        public Dictionary<string, int> Portfolio { get; set; } = new();

        public void AddStock(string isin, int quantity)
        {
            if (Portfolio.ContainsKey(isin))
                Portfolio[isin] += quantity;
            else
                Portfolio[isin] = quantity;
        }

        public void SellStock(string isin, int quantity)
        {
            if (HasStock(isin, quantity))
            {
                Portfolio[isin] -= quantity;
                if (Portfolio[isin] == 0) Portfolio.Remove(isin);
            }
        }

        public bool HasStock(string isin, int quantity)
        {
            return Portfolio.ContainsKey(isin) && Portfolio[isin] >= quantity;
        }
    }
}
