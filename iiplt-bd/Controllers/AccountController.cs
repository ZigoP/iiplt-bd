using Microsoft.AspNetCore.Mvc;
using iiplt_bd.Models;
using iiplt_bd.Services;
using System.Linq;

[ApiController]
[Route("api")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("account")]
    public ActionResult<AccountState> GetAccountState()
    {
        return Ok(_accountService.GetAccountState());
    }

    [HttpGet("stocks")]
    public ActionResult<List<Stock>> GetStocks()
    {
        return Ok(_accountService.GetStocks());
    }

    [HttpPost("trade")]
    public IActionResult Trade([FromBody] TradeRequest request)
    {
        var result = _accountService.ProcessTrade(request);
        if (result.Success)
            return Ok(new { message = result.Message, account = result.Account });

        return BadRequest(result.Message);
    }
}
