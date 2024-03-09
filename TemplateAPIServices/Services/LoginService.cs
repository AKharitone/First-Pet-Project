using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TemplateAPIModel;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIServices.Services {
  public class LoginService : ILoginService {
    public const string Issuer = "www.memos.cz";
    public const string Audience = "www.template.cz";
    const int TokenExpirationInMinutes = 14 * 24 * 60; // 14 dní

    private readonly TemplateContext _context;
    private readonly IApplicationConfiguration _configuration;

    public LoginService(IApplicationConfiguration configuration, TemplateContext context) {
      _configuration = configuration;
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<string> CreateJwtToken(LoginModel model) {
      if (!await IsValid(model)) {
        throw new InvalidLoginException(); //The SecurityException to be handled by a global exception handler in the API to return 401 Unauthorized
      }
      else {
        int lockedFor = 0;

        if (lockedFor == 0) {
        }
        else
          throw new AccountLockedException(lockedFor);
      }

      //Create the list of claims
      var claims = new[]
      {
                new Claim(ClaimTypes.Name, model.UserName)
            };

      //Create the JWT token
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecurityKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: Issuer,
          audience: Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<bool> IsValid(LoginModel model) {
      var user = await _context.User.SingleOrDefaultAsync(x =>
      x.UserName.ToLower() == model.UserName.ToLower() &&
      x.PasswordHash == Tools.GetHash(model.Password + x.UserName + x.PasswordSalt));

      return (user != null);
    }
  }

  public class InvalidLoginException : SecurityException { }
  public class AccountLockedException : SecurityException {
    public AccountLockedException(int duration) : base(duration.ToString()) { }
  }
}