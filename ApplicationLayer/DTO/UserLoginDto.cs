using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public  class UserLoginDto
    {
        [Column(TypeName = "VARCHAR(16)")]
        public string? Username { get; set; }

        [Column(TypeName = "VARCHAR(10)")]
        public string? Password { get; set; }
    }
}
