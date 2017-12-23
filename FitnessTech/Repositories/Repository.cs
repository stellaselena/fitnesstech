using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitnessTech.Data;
using FitnessTech.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FitnessContext _context;

        public Repository(FitnessContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {

            return await EntityFrameworkQueryableExtensions.ToListAsync(_context.Set<T>());
        }

        public virtual T Get(int? id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {

            _context.Set<T>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;

        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return Queryable.SingleOrDefault(_context.Set<T>(), match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_context.Set<T>(), match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return Queryable.Where(_context.Set<T>(), match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await Queryable.Where(_context.Set<T>(), match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                _context.SaveChanges();
            }
            return exist;
        }

        public void Update(T t)
        {
             _context.Update(t);
        }
    

        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(t);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return Queryable.Count(_context.Set<T>());
        }

        public async Task<int> CountAsync()
        {
            return await EntityFrameworkQueryableExtensions.CountAsync(_context.Set<T>());
        }

        public virtual void Save()
        {

            _context.SaveChanges();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;

        }

        public void AddEntity(T t)
        {
            _context.Add(t);
        }

        public T GetEntityWith(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Queryable.Where(_context.Set<T>(), predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await Queryable.Where(_context.Set<T>(), predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = GetAll();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return await result.FirstOrDefaultAsync(predicate);
        }

        public bool Exists(object key)
        {

            return _context.Set<T>().Find(key) != null;
        }
    }

}

