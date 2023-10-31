using _4PawsBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace PawsBackend.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Termin> Termin { get; set; }
        public DbSet<Salon> SalonSlike { get; set; }
        public DbSet<Galery> GalerySlike { get; set; }
        //public DbSet<Prodavci> Prodavci { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Termin>()
            //.Property(e => e.Datum)
            //.HasColumnType("date");


            base.OnModelCreating(modelBuilder);
        }
    }
}
