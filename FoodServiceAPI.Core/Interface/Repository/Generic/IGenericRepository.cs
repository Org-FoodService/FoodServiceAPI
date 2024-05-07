namespace FoodServiceAPI.Core.Interface.Repository.Generic
{
    public interface IGenericRepository<T, TKey>
        where T : class
        where TKey : struct
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        T GetById(TKey id);
        Task<T> GetByIdAsync(TKey id);
        Task<int> InsertOrUpdateAsync(T entity);
        IQueryable<T> Query();
        Task<int> UpdateAsync(T entity);
        IQueryable<T> ListAll();
    }
}