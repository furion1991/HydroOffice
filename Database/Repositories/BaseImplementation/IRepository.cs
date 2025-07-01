using HydroOffice.Database.Models;

namespace HydroOffice.Database.Repositories.BaseImplementation;

public interface IRepository<T> where T : class
{
    Task<IList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<List<Order>> GetOrdersByEmployeeAsync(Employee employee);
    Task<List<Order>> GetOrdersByContractorAsync(Contractor contractor);
    Task<List<Contractor>> GetContractorsByEmployeeAsync(Employee employee);
}