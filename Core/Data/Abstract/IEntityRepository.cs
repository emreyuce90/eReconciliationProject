using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Data.Abstract
{
    public interface IEntityRepository<T> where T : class,IEntity,new()
    {
        //Table
        public DbSet<T> Table { get; set; }
        //Ekleme
        Task<bool> AddAsync(T entity);
        //Güncelleme
        bool Update(T entity);
        //Silme
        Task<bool> DeleteAsync(int id);
        bool Delete(T entity);
        Task<int> SaveChangesAsync();
        //Sorgular
        IQueryable<T> GetAll(bool isTracking=true);
        IQueryable<T> GetAllByFilter(Expression<Func<T,bool>> expression,bool isTracking=true);
        Task<T> GetSingle(Expression<Func<T,bool>> expression, bool isTracking = true);
        Task<T> GetById(int id,bool isTracking=true);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
        Task<int> CountAsync(Expression<Func<T, bool>> expression, bool isTracking = true);
    }
}
