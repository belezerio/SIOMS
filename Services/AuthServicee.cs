using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SIOMS.Data;
using SIOMS.Models;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context , IConfiguration config)
    {
        _context = context ;
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
          new Claim(ClaimTypes.Name , user.Username),
          new Claim(ClaimTypes.Role, user.Role)  
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

        var tokens = new JwtSecurityToken(
            issuer : _config["Jwt:Issuer"],
            audience : _config["Jwt:Audience"],
            claims: claims,
            expires : DateTime.Now.AddHours(2),
            signingCredentials : creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokens);
    }
    public User ValidateUser(string username , string  password)
    {
        return _context.Users.FirstOrDefault(u =>
            u.Username == username && u.PasswordHash == password);
        
    }
}