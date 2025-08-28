namespace ReadingTracker.API.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}