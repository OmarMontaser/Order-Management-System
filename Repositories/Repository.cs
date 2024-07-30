using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync();
        }

        public async Task <IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T>  FindAsync(Expression<Func<T, bool>> match)
        {
             return await _context.Set<T>().SingleOrDefaultAsync(match);
        }


        public async Task<T> FindAsync(Expression<Func<T, bool>> match , string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes != null)
                foreach(var include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(match);
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(match).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match, int take, int skip)
        {
            return await _context.Set<T>().Where(match).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        T  UpdateAsync(T Entity)
        {
            _context.Update(Entity);
            return  Entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
