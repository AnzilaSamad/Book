using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class UserRegisterDto
    {
        [Key]
        [Column(TypeName = "VARCHAR")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Username should be atleast 8 characters")]
        public string? Username { get; set; }

        [Column(TypeName = "VARCHAR(25)")]
        public string? Email { get; set; }

        [Column(TypeName = "DOUBLE")]
        public  double PhoneNumber { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string? Password { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
