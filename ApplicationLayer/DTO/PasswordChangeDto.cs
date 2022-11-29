using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class PasswordChangeDto
    {
        [Key]
        public string? Username { get; set; }

       
        public string? Email { get; set; }

   
        public double PhoneNumber { get; set; }


        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
}
