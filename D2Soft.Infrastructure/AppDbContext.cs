using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2Soft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace D2Soft.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure FinancialAccount entity
            modelBuilder.Entity<FinancialAccount>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<FinancialAccount>()
                .Property(f => f.AccountNumber)
                .HasMaxLength(100);  

            modelBuilder.Entity<FinancialAccount>()
                .Property(f => f.AccountType)
                .HasMaxLength(50);  

            modelBuilder.Entity<FinancialAccount>()
                .Property(f => f.Balance)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<FinancialAccount>()
                .HasOne(f => f.Owner) // A FinancialAccount has one Owner (User)
                .WithMany() // A User may have many FinancialAccounts
                .HasForeignKey(f => f.OwnerId) // foreign key property
                .IsRequired(false) // Foreign key is nullable
                .OnDelete(DeleteBehavior.SetNull); // Delete behavior if a user is deleted (Set to null)

            // Configure User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(100);  

            modelBuilder.Entity<User>()
                .Property(u => u.UserEmail)
                .HasMaxLength(255);  
        }
    }
}
