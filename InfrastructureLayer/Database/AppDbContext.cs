using DomainLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InfrastructureLayer
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions <AppDbContext> dboptions) : base(dboptions)
        {
        }
        public DbSet<UserRegister>? UserRegisterDetails { get; set; }    
        

    }


}
