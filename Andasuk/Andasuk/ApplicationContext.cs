using Andasuk.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andasuk
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CarProduct> CarProducts { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Spare> Spares { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Andrasuk;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //CarProduct
            modelBuilder
                .Entity<CarProduct>()
                .HasOne(e => e.Car)
                .WithMany(e => e.CarProducts)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<CarProduct>()
                .HasOne(e => e.Product)
                .WithMany(e => e.CarProducts)
                .OnDelete(DeleteBehavior.NoAction);

            //Product
            modelBuilder
                .Entity<Product>()
                .HasOne(e => e.Creator)
                .WithMany(e => e.Products)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Product>()
                .HasOne(e => e.Spare)
                .WithMany(e => e.Products)
                .OnDelete(DeleteBehavior.NoAction);

            //Spare

            modelBuilder
                .Entity<Spare>()
                .HasOne(e => e.Catalog)
                .WithMany(e => e.Spares)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
