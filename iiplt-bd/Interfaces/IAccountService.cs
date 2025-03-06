using iiplt_bd.Models;

namespace iiplt_bd.Services
{
    public interface IAccountService
    {
        AccountState GetAccountState();
        List<Stock> GetStocks();
        (bool Success, string Message, AccountState Account) ProcessTrade(TradeRequest request);
    }
}
