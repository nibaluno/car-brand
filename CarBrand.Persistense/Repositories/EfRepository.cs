namespace CarBrand.Persistense.Repositories;

using System.Linq.Expressions;
using CarBrand.Persistense.Data;

internal class EfRepository<T> : IRepository<T> where T : Domain.Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities; 

    public EfRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>(); 
    }

    public async Task<T> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T> query = _entities.AsQueryable();

        if (includesProperties != null)
            foreach (var include in includesProperties)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T> query = _entities.AsQueryable();

        if (includesProperties != null)
            foreach (var include in includesProperties)
                query = query.Include(include);

        if (filter != null)
            query = query.Where(filter);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await _entities.FirstOrDefaultAsync(filter, cancellationToken);
    }
}