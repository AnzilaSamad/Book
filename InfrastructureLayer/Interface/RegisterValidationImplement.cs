using ApplicationLayer.DTO;
using DomainLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Interface
{
    public class RegisterValidationImplement : IRegisterValidation
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public RegisterValidationImplement(IConfiguration configuration, AppDbContext appDbContext)
        {

            _configuration = configuration;
            _appDbContext = appDbContext;
        }

        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            }
        }

        public string CreateRandomPassword()
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public string CreateToken(UserRegister user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
               
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.
                    ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                return computedHash.SequenceEqual(PasswordHash);

            }
        }

        public void Edit(PasswordChangeDto passwordChangeDto)
        {

            UserRegister emp = _appDbContext.UserRegisterDetails.FirstOrDefault(i => i.Email == passwordChangeDto.Email);
            if (emp != null)
            {
                emp.PasswordHash = passwordChangeDto.PasswordHash;
                emp.PasswordSalt = passwordChangeDto.PasswordSalt;
                _appDbContext.UserRegisterDetails.Update(emp);
                _appDbContext.SaveChanges();
            }
        }





    }
}
