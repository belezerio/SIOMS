

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var user = _authService.ValidateUser(dto.Username, dto.Password);

        if(user == null)
        {
            return Unauthorized("Invalid credentials");
        }
        var token = _authService.GenerateToken(user);

        return Ok(new {token});
    }

}