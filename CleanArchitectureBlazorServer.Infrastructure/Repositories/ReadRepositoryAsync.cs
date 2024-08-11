using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Contracts;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Infrastructure.Repositories
{
    public class ReadRepositoryAsync<T, TId> : IReadRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        private readonly ApplicationDbContext _context;

        public ReadRepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(TId id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
