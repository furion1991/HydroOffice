using NHibernate;
using NHibernate.Linq;

namespace HydroOffice.Database.Repositories.BaseImplementation;

public class Repository<T>(ISession session) : IRepository<T>
    where T : class
{
    private readonly ISession _session = session ?? throw new ArgumentNullException(nameof(session));

    public async Task<IList<T>> GetAllAsync()
    {
        return await _session.Query<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _session.GetAsync<T>(id);
    }

    public async Task AddAsync(T entity)
    {
        using var tx = _session.BeginTransaction();
        await _session.SaveAsync(entity);
        await tx.CommitAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        using var tx = _session.BeginTransaction();
        await _session.UpdateAsync(entity);
        await tx.CommitAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        using var tx = _session.BeginTransaction();
        await _session.DeleteAsync(entity);
        await tx.CommitAsync();
    }
}