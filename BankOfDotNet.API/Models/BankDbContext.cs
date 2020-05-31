using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankOfDotNet.API.Models
{
    public class BankDbContext  : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }


    }
}
