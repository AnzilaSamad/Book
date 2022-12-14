using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public  class UserRegister
    {
        [Key]
       
        //[Column(TypeName = "varchar")]
        //[StringLength(16, MinimumLength = 8, ErrorMessage = "Username should be atleast 8 characters")]
        public string? Username { get; set; }
      
        //[Column(TypeName = "varchar(25)")]
        public string Email { get; set; }
       
        //[Column(TypeName = "DOUBLE")]
        public double PhoneNumber { get; set; }

     
        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
}
