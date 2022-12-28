using Core.Data.Abstract;
using Core.Entities;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Core.Data.Concrete
{
    public class EfEntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {

        private readonly eReconciliationDb _context;

        public EfEntityRepository(eReconciliationDb context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.AnyAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.CountAsync(expression);
        }

        public bool Delete(T entity)
        {
            EntityEntry<T> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T? deleted = await Table.FindAsync(id);
            if (deleted != null)
            {
                EntityEntry<T> entityEntry = Table.Remove(deleted);
                return entityEntry.State == EntityState.Deleted;

            }
            return false;
        }

        public IQueryable<T> GetAll(bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query;

        }

        public IQueryable<T> GetAllByFilter(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return query.Where(expression);
        }


        public async Task<T> GetSingle(Expression<Func<T, bool>> expression, bool isTracking = true)
        {
            var query = Table.AsQueryable();
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
