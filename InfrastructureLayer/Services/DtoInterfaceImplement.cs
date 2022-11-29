using ApplicationLayer.DTO;
using ApplicationLayer.Interface;
using AutoMapper;
using DomainLayer;
using InfrastructureLayer.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Services
{
    public class DtoInterfaceImplement : IRegister
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRegisterValidation _registerValidation;
      
        public DtoInterfaceImplement(AppDbContext appDbContext, IRegisterValidation registerValidation)
        {
            _appDbContext = appDbContext;
            _registerValidation = registerValidation;   
            
        }

        public string Add(UserRegisterDto userRegisterDto)
        {
            if (_appDbContext.UserRegisterDetails.Any(u => u.Username == userRegisterDto.Username))
            {
                return"User already exist";
            }
            else if(_appDbContext.UserRegisterDetails.Any(u => u.Email == userRegisterDto.Email))
            {
                return "Email already exist";
            }
            else if (_appDbContext.UserRegisterDetails.Any(u => u.PhoneNumber == userRegisterDto.PhoneNumber))
            {
                return "Phone number already exist";
            }


            _registerValidation.CreatePasswordHash(userRegisterDto.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
                var user = new UserRegister()
                {
                   
                    Username = userRegisterDto.Username,
                    Email = userRegisterDto.Email,
                    PhoneNumber = userRegisterDto.PhoneNumber,
                    PasswordHash = PasswordHash,
                    PasswordSalt = PasswordSalt,

                };
                _appDbContext.UserRegisterDetails.Add(user);
                _appDbContext.SaveChanges();
           return "OK";
            
        }

      

        public List<UserRegister> Get()
        {
            return _appDbContext.UserRegisterDetails.ToList();
        }

        
    }
}
