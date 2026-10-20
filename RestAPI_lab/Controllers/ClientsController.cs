using Microsoft.AspNetCore.Mvc;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Abstractions;
using Microsoft.AspNetCore.Authorization;
using BankDepositsApi.Data;
using BankDepositsApi.Models;

namespace RestAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ICommonService<Client, int> _clientsService;

    public ClientsController(ICommonService<Client, int> clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetAll()
    {
        var clients = await _clientsService.GetAll();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> Get(int id)
    {
        var client = await _clientsService.Get(id);
        if (client == null) return NotFound("Клиент не найден");
        return Ok(client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client)
    {
        var success = _clientsService.Update(id, client);
        if (!success) return BadRequest("Не удалось обновить данные клиента");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var success = _clientsService.Delete(id);
        if (!success) return NotFound("Клиент не найден");
        return NoContent();
    }
}
