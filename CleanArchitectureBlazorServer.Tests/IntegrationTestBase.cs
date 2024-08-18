using CleanArchitectureBlazorServer.Application.Features.AccountHolders.Commands;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureBlazorServer.Web.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureBlazorServer.Tests
{
    public class IntegrationTestBase : IDisposable
    {
        protected  ApplicationDbContext _dbContext;
        protected  IServiceScope _scope;
        protected readonly DbConnection _connection;
        private bool _disposed = false; // Flag to detect redundant calls

        public IntegrationTestBase()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Add other services as needed, e.g., AutoMapper, MediatR
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAccountHolderCommandHandler).Assembly));

            var serviceProvider = services.BuildServiceProvider();
            _scope = serviceProvider.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure the database is created
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext?.Dispose();
                    _connection?.Dispose();
                    _scope?.Dispose();
                }

                _disposed = true;
            }
        }

        ~IntegrationTestBase()
        {
            Dispose(false);
        }
    }

}
