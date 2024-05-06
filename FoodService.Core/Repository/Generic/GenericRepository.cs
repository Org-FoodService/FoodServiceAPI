using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using FoodService.Data.Context;
using System.Linq;
using FoodService.Core.Interface.Repository.Generic;

namespace FoodService.Core.Repository.Generic
{
    /// <summary>
    /// Generic repository implementation for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class where TKey : struct
    {
        public readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the GenericRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        protected GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        public virtual async Task<T> CreateAsync(T entity)
        {
            EntityEntry<T> ret = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;
            return ret.Entity;
        }

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        public virtual async Task<int> UpdateAsync(T entity)
        {
            EntityEntry<T>? entry = _context.Entry(entity) ?? throw new KeyNotFoundException("Entity not found");
            entry.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts or updates an entity asynchronously.
        /// </summary>
        public virtual async Task<int> InsertOrUpdateAsync(T entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            EntityEntry<T>? entry = _context.Entry(entity) ?? throw new KeyNotFoundException("Entity not found");
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        public virtual T GetById(TKey id)
        {
            return _context.Set<T>().Find(id)!;
        }

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        /// <summary>
        /// Returns a queryable collection of entities.
        /// </summary>
        public virtual IQueryable<T> Query()
        {
            return _context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Returns a queryable collection of all entities.
        /// </summary>
        public virtual IQueryable<T> ListAll()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
