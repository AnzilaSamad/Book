using ApplicationLayer.DTO;
using ApplicationLayer.Interface;
using InfrastructureLayer.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Services
{
    public class LoginInterfaceImplement : ILogin
    {

        private readonly IRegisterValidation _registerValidation;
        private readonly AppDbContext _appDbContext;
        public LoginInterfaceImplement(IRegisterValidation registerValidation, AppDbContext appDbContext)
        {
            _registerValidation = registerValidation;
            _appDbContext = appDbContext;   
        }
        public string Login(UserLoginDto userLoginDto)
        {
            var user = _appDbContext.UserRegisterDetails.FirstOrDefault(u => u.Username == userLoginDto.Username);
            if (user == null)
            {
                return "user not found";
            }
            if (!_registerValidation.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return "wrong password";
            }

            string token = _registerValidation.CreateToken(user);

            return token;

        }
    }
}
