using CleanArchitectureBlazorServer.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Infrastructure.Contexts
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)

    {
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<AccountHolder> AccountHolders => Set<AccountHolder>();
        //public DbSet<Person> People => Set <Person>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
