using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using FoodServiceAPI.Data.SqlServer.Context;

namespace FoodServiceAPI.Data.SqlServer.Repository.Generic
{
    /// <summary>
    /// Generic repository implementation for CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class where TKey : struct
    {
        private readonly AppDbContext _context;
        private readonly ILogger<GenericRepository<T, TKey>> _logger;

        /// <summary>
        /// Initializes a new instance of the GenericRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance.</param>
        protected GenericRepository(AppDbContext context, ILogger<GenericRepository<T, TKey>> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new entity asynchronously.
        /// </summary>
        public virtual async Task<T> CreateAsync(T entity)
        {
            _logger.LogInformation("Creating a new entity.");
            EntityEntry<T> ret = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            ret.State = EntityState.Detached;
            _logger.LogInformation("Entity created successfully.");
            return ret.Entity;
        }

        /// <summary>
        /// Updates an existing entity asynchronously.
        /// </summary>
        public virtual async Task<int> UpdateAsync(T entity)
        {
            _logger.LogInformation("Updating an entity.");
            EntityEntry<T>? entry = _context.Entry(entity) ?? throw new KeyNotFoundException("Entity not found");
            entry.State = EntityState.Modified;
            int result = await _context.SaveChangesAsync();
            _logger.LogInformation("Entity updated successfully.");
            return result;
        }

        /// <summary>
        /// Inserts or updates an entity asynchronously.
        /// </summary>
        public virtual async Task<int> InsertOrUpdateAsync(T entity)
        {
            _logger.LogInformation("Inserting or updating an entity.");
            _context.Update(entity);
            int result = await _context.SaveChangesAsync();
            _logger.LogInformation("Entity inserted or updated successfully.");
            return result;
        }

        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _logger.LogInformation("Deleting an entity.");
            EntityEntry<T>? entry = _context.Entry(entity) ?? throw new KeyNotFoundException("Entity not found");
            entry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Entity deleted successfully.");
            return true;
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        public virtual T GetById(TKey id)
        {
            _logger.LogInformation("Retrieving an entity by ID: {Id}.", id);
            T entity = _context.Set<T>().Find(id)!;
            if (entity == null)
            {
                _logger.LogWarning("Entity not found for ID: {Id}.", id);
            }
            else
            {
                _logger.LogInformation("Entity retrieved successfully for ID: {Id}.", id);
            }
            return entity;
        }

        /// <summary>
        /// Retrieves an entity by its ID asynchronously.
        /// </summary>
        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            _logger.LogInformation("Retrieving an entity asynchronously by ID: {Id}.", id);
            T entity = (await _context.Set<T>().FindAsync(id))!;
            if (entity == null)
            {
                _logger.LogWarning("Entity not found for ID: {Id}.", id);
            }
            else
            {
                _logger.LogInformation("Entity retrieved successfully for ID: {Id}.", id);
            }
            return entity;
        }

        /// <summary>
        /// Returns a queryable collection of entities.
        /// </summary>
        public virtual IQueryable<T> Query()
        {
            _logger.LogInformation("Querying entities.");
            return _context.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Returns a queryable collection of all entities.
        /// </summary>
        public virtual IQueryable<T> ListAll()
        {
            _logger.LogInformation("Listing all entities.");
            return _context.Set<T>().AsQueryable();
        }
    }
}
