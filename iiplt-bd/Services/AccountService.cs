using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using iiplt_bd.Models;

namespace iiplt_bd.Services
{
    public class AccountService : IAccountService
    {
        private const string FilePath = "account.json";
        private AccountState _account;
        private List<Stock> Stocks;

        public AccountService()
        {
            _account = LoadAccountState();
            Stocks = new List<Stock>
            {
                new Stock("NVIDIA Corp", "US67066G1040", 131),
                new Stock("Apple Inc", "US0378331005", 240),
                new Stock("Amazon.com Inc", "US0231351067", 214)
            };
        }

        public AccountState GetAccountState() => _account;

        public List<Stock> GetStocks() => Stocks;

        public (bool Success, string Message, AccountState Account) ProcessTrade(TradeRequest request)
        {
            var stock = Stocks.FirstOrDefault(s => s.ISIN == request.ISIN);
            if (stock == null) return (false, "Stock not found", _account);

            if (request.Type == TradeType.BUY)
            {
                var totalCost = request.Quantity * stock.Price;
                if (_account.Balance >= totalCost)
                {
                    _account.Balance -= totalCost;
                    _account.AddStock(request.ISIN, request.Quantity, Stocks);  
                    SaveAccountState();
                    return (true, "Purchase successful", _account);
                }
                return (false, "Insufficient funds", _account);
            }
            else if (request.Type == TradeType.SELL)
            {
                if (_account.HasStock(request.ISIN, request.Quantity))
                {
                    var totalValue = request.Quantity * stock.Price;
                    _account.Balance += totalValue;
                    _account.SellStock(request.ISIN, request.Quantity, stock.Price);
                    SaveAccountState();
                    return (true, "Sale successful", _account);
                }
                return (false, "Not enough shares", _account);
            }

            return (false, "Invalid trade type", _account);
        }


        private AccountState LoadAccountState()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    return JsonSerializer.Deserialize<AccountState>(json) ?? new AccountState();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading account state: {ex.Message}");
            }
            return new AccountState(); 
        }


        private void SaveAccountState()
        {
            try
            {
                var json = JsonSerializer.Serialize(_account, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving account state: {ex.Message}");
            }
        }
    }
}
