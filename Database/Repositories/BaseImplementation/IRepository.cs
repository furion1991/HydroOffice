namespace HydroOffice.Database.Repositories.BaseImplementation;

public interface IRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}