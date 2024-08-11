using CleanArchitectureBlazorServer.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Application.Repositories
{
    public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(TId id);
        IQueryable<T> Entities { get; }
    }
}
