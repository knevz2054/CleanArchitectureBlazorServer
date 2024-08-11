﻿using CleanArchitectureBlazorServer.Application.Repositories;
using CleanArchitectureBlazorServer.Common.Contracts;
using CleanArchitectureBlazorServer.Infrastructure.Contexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureBlazorServer.Infrastructure.Repositories
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;
        private Hashtable _repositories = new Hashtable();
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = $"{typeof(T).Name}_Read";

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(ReadRepositoryAsync<,>);
                var repositoryInstance = Activator
                    .CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IReadRepositoryAsync<T, TId>)_repositories[type];
        }

        public IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = $"{typeof(T).Name}_Write";

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(WriteRepositoryAsync<,>);
                var repositoryInstance = Activator
                    .CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IWriteRepositoryAsync<T, TId>)_repositories[type]; ;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }
    }
}
