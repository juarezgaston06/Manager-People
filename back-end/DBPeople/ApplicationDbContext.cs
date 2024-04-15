using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DBPeople
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<Person> People { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.DNI)
                .IsUnique();

            modelBuilder.Entity<Province>().HasData(
         new Province { Id = 1, Name="Cordoba"},
         new Province { Id = 2, Name = "Buenos Aires" },
         new Province { Id = 3, Name = "Formosa" },
         new Province { Id = 4, Name = "Entre Rios" },
         new Province { Id = 5, Name = "San Luis" }

     );
        }
    }
}
