using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BankDepositsApi.Models;
using BankDepositsApi.Models.Services; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankDepositsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var client = _authService.Login(request.Login, request.Password);
        if (client == null) return Unauthorized("Неверный логин или пароль");

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, client.Login) };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Ok(new { token = encodedJwt, clientName = client.FullName });
    }

    [HttpPost("register")]
    public IActionResult Register(Client client)
    {
        var result = _authService.Register(client);
        if (!result) return BadRequest("Пользователь с таким логином уже существует");
        return Ok("Регистрация успешна");
    }
}

public class LoginRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
