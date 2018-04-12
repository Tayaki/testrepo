using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPages.Models
{
    public class DogContext : DbContext
    {
        public DogContext(DbContextOptions<DogContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dog { get; set; }
        //public DbSet<Cat> Cat { get; set; }
    }
}
