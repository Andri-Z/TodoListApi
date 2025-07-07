using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoListApi.Context;
using TodoListApi.Models;
using TodoListApi.Models.Jwt;

namespace TodoListApi.Services
{
    public class JwtServices
    {
        private readonly TodoListContext _context;
        private readonly IConfiguration _configuration;
        public JwtServices(TodoListContext context, IConfiguration configuration) =>
        (_context, _configuration) = (context, configuration);

        public async Task<LoginResponseModel?> Authenticate(LoginRequestModel request)
        {
            HashServices hash = new();

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                return null;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user is null || !hash.verifyPassword(request.Password,user.Password))
                return null;

            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpityTimeSpan = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email,request.Email)
                }),
                Expires = tokenExpityTimeSpan,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accesToken = tokenHandler.WriteToken(securityToken);

            return new LoginResponseModel
            {
                Email = request.Email,
                AccesToken = accesToken,
                ExpiresIn = (int)tokenExpityTimeSpan.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }
        public async Task<LoginResponseModel?> RegisterUser(RegisterRequestModel request)
        {
            HashServices hash = new();

            if (string.IsNullOrWhiteSpace(request.Name.Trim()) || 
                string.IsNullOrWhiteSpace(request.Email.Trim()) || 
                string.IsNullOrWhiteSpace(request.Password.Trim()))
                return null;

            var emailExist = await _context.Users.AnyAsync(x => x.Email == request.Email);
            if (emailExist)
                return null;

            var passwordHashed = hash.HashPassword(request.Password);

            var newUser = new Users
            {
                Name = request.Name,
                Email = request.Email,
                Password = passwordHashed
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return null;   
        }
    }
}
