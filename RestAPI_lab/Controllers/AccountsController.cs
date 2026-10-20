using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankDepositsApi.Data;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;


namespace RestAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientAccountsController : ControllerBase
{
    private readonly ICommonService<Account, string> _accountsService;

    public ClientAccountsController(ICommonService<Account, string> accountsService)
    {
        _accountsService = accountsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetAll()
    {
        var accounts = await _accountsService.GetAll();
        return Ok(accounts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> Get(string id)
    {
        var account = await _accountsService.Get(id);
        if (account == null) return NotFound("Счет не найден");
        return Ok(account);
    }

    [HttpPost]
    public IActionResult Create(Account account)
    {
        var success = _accountsService.Create(account);
        if (!success) return BadRequest("Не удалось открыть счет. Проверьте правильность Кода Клиента и Кода Вклада.");
        return CreatedAtAction(nameof(Get), new { id = account.AccountNumber }, account);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var success = _accountsService.Delete(id);
        if (!success) return NotFound("Счет не найден");
        return NoContent();
    }
}
