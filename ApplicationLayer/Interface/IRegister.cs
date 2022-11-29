using ApplicationLayer.DTO;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interface
{
    public interface IRegister
    {
        string Add(UserRegisterDto userRegisterDto);

        List<UserRegister> Get();
        
       

    }
}
