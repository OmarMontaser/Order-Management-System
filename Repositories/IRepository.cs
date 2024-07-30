using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T> where T : class 
    {
        Task<T> GetByIdAsync(int id);
        Task <IEnumerable<T>> GetAllAsync();        
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int take , int skip);
        Task<T> AddAsync(T Entity);
        T Update(T entity);
        void Delete(T entity);

    }
}
