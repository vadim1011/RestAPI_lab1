using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;

namespace BankDepositsApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DepositsController : ControllerBase
{
    private readonly ICommonService<Deposit, int> _depositsService;

    public DepositsController(ICommonService<Deposit, int> depositsService)
    {
        _depositsService = depositsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Deposit>>> GetAll()
    {
        var deposits = await _depositsService.GetAll();
        return Ok(deposits);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Deposit>> Get(int id)
    {
        var deposit = await _depositsService.Get(id);
        if (deposit == null) return NotFound("Вид вклада не найден");
        return Ok(deposit);
    }

    [HttpPost]
    public IActionResult Create(Deposit deposit)
    {
        var success = _depositsService.Create(deposit);
        if (!success) return BadRequest("Не удалось создать вид вклада");
        return CreatedAtAction(nameof(Get), new { id = deposit.Id }, deposit);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _depositsService.Delete(id);
        if (!success) return NotFound("Вид вклада не найден или не может быть удален");
        return NoContent();
    }
}
