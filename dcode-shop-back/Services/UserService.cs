using dcode_shop_back.Data;
using dcode_shop_back.Helpers;
using dcode_shop_back.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dcode_shop_back.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ShopContext _shopContext;
        public UserService(IOptions<AppSettings> appSettings, ShopContext shopContext)
        {
            _appSettings = appSettings.Value;
            _shopContext = shopContext;
        }
        public User Authenticate(string email, string password)
        {
            var user = _shopContext.Users.Include(u => u.Customer).SingleOrDefault(x => x.Email == email && x.Password == password);
            // return null if user not found
            if (user == null)
                return null;
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Email", user.Customer.Email),
                    new Claim("isActive", user.IsActive.ToString()),
                    new Claim("isAdmin", user.IsAdmin.ToString()),
                    new Claim("isSuperAdmin", user.IsSuperAdmin.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            // remove password before returning
            user.Password = null;
            return user;
        }
    }
}
