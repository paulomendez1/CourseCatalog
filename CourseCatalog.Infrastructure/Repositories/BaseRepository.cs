using CourseCatalog.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly CourseCatalogContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public BaseRepository(CourseCatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken token)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(token);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("GetById operation was canceled.");
            }
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null) _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public T Add(T entity, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Add operation was canceled.");
            }
            return _context.Set<T>().Add(entity).Entity;
        }

        public T Update(T entity, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Add operation was canceled.");
            }
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}

