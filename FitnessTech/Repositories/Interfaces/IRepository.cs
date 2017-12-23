using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FitnessTech.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
      
        T Add(T t);
        Task<T> AddAsync(T t);
        int Count();
        Task<int> CountAsync();
        void Delete(T entity);
        Task<int> DeleteAsync(T entity);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        T Get(int? id);
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(int? id);
        void Save();
        Task<int> SaveAsync();
        T Update(T t, object key);
        void Update(T t);
        Task<T> UpdateAsync(T t, object key);
        bool SaveAll();
        void AddEntity(T t);
        Task<T> GetBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        bool Exists(object key);



    }
}
 