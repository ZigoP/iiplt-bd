﻿using System.Linq;
using System.Collections.Generic;

namespace iiplt_bd.Models
{
    public class AccountState
    {
        public decimal Balance { get; set; } = 10000;
        public List<OwnedStock> Portfolio { get; set; }

        public AccountState()
        {
            Portfolio = new List<OwnedStock>
        {
            new OwnedStock
            {
                Stock = new Stock("NVIDIA Corp", "US67066G1040", 131),
                Quantity = 10,
                TotalPrice = 1310
            },
            new OwnedStock
            {
                Stock = new Stock("Apple Inc", "US0378331005", 240),
                Quantity = 5,
                TotalPrice = 1200
            }
        };
        }

        public void AddStock(string isin, int quantity, List<Stock> stocks)
        {
            var stock = stocks.FirstOrDefault(s => s.ISIN == isin);
            if (stock == null)
            {
                throw new Exception("Stock not found");
            }

            var ownedStock = Portfolio.FirstOrDefault(s => s.Stock.ISIN == isin);
            if (ownedStock != null)
            {
                ownedStock.Quantity += quantity;
                ownedStock.TotalPrice += stock.Price * quantity;
            }
            else
            {
                Portfolio.Add(new OwnedStock
                {
                    Stock = stock,
                    Quantity = quantity,
                    TotalPrice = stock.Price * quantity
                });
            }
        }

        public void SellStock(string isin, int quantity, decimal price)
        {
            var ownedStock = Portfolio.FirstOrDefault(s => s.Stock.ISIN == isin);
            if (ownedStock != null && ownedStock.Quantity >= quantity)
            {
                ownedStock.Quantity -= quantity;
                ownedStock.TotalPrice -= price * quantity;

                if (ownedStock.Quantity == 0)
                {
                    Portfolio.Remove(ownedStock);
                }
            }
        }

        public bool HasStock(string isin, int quantity)
        {
            var ownedStock = Portfolio.FirstOrDefault(s => s.Stock.ISIN == isin);
            return ownedStock != null && ownedStock.Quantity >= quantity;
        }
    }
}
