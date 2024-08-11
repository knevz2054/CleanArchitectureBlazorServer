using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using CleanArchitectureBlazorServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Infrastructure
{
    public static class StartUp
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("MyConnection")));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>))
                .AddTransient(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        }
    }
}
