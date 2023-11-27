using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Domain.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        Task<T> GetByIdAsync(int id, CancellationToken token);
        T Add(T entity, CancellationToken token);
        T Update(T entity, CancellationToken token);
    }
}
