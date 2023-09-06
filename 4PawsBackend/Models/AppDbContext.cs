using _4PawsBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawsBackend.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Termin> Termin { get; set; }
        //public DbSet<Prodavci> Prodavci { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Prodavnica>().HasData(
            //    new Prodavnica() { Id = 1, Name = "Maxi", Adress="Kralja petra 1 78" },
            //    new Prodavnica() { Id = 2, Name = "Idea", Adress="Novosadski put 26" }

            //);

            //modelBuilder.Entity<Prodavci>().HasData(
            //    new Prodavci()
            //    {
            //        Id = 1,
            //        Name = "Pera",
            //        LastName="Peric",
            //        BirthYear = 1985,
            //        ProdavnicaId = 1,
            //    },
            //    new Prodavci()
            //    {
            //        Id = 2,
            //        Name = "Zika",
            //        LastName = "Zikic",
            //        BirthYear = 1990,
            //        ProdavnicaId = 1,
            //    },
            //    new Prodavci()
            //    {
            //        Id = 3,
            //        Name = "Mika",
            //        LastName = "Mikic",
            //        BirthYear = 1985,
            //        ProdavnicaId = 2,
            //    }

            //);

            base.OnModelCreating(modelBuilder);
        }
    }
}
