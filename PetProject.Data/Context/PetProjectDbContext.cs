using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Data.Context
{
    public class PetProjectDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ConfirmEmailToken> ConfirmEmailTokens { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Lending> Lendings { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTravel> UserTravels { get; set; }
        public DbSet<Payments> Payments { get; set; }

        public PetProjectDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           /* builder.Entity<ApplicationUser>()
          .HasMany(u => u.UserTravel)
          .WithOne(ut => ut.Client)
          .HasForeignKey(ut => ut.ClientId);

            builder.Entity<UserTravel>()
                .HasOne(ut => ut.Client)
                .WithMany(u => u.UserTravel)
                .HasForeignKey(ut => ut.ClientId);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.UserTravel)
                .WithOne(ut => ut.Driver)
                .HasForeignKey(ut => ut.DriverId);

            builder.Entity<UserTravel>()
                .HasOne(ut => ut.Driver)
                .WithMany(u => u.UserTravel)
                .HasForeignKey(ut => ut.DriverId);

            // Define the many-to-many relationship using a junction table
            builder.Entity<UserTravel>()
                .HasKey(ut => new { ut.DriverId, ut.ClientId });

            // Rest of your entity configurations...*/

            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        public static void SeedRoles(ModelBuilder modelBuilder)
        {
            /*if (!modelBuilder.Model.GetEntityTypes().Any(et => et.Name == typeof(IdentityRole).FullName))
            {*/
                modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "ADMIN", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "USER", ConcurrencyStamp = "2", NormalizedName = "USER" },
            new IdentityRole() { Name = "RIDER", ConcurrencyStamp = "3", NormalizedName = "RIDER" });
           /* }*/
        }
    }
    
}
