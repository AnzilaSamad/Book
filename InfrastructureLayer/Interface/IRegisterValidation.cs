using ApplicationLayer.DTO;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Interface
{
    public interface IRegisterValidation
    {
        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
       
        public bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt);

        

    }
}
