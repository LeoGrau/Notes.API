using Microsoft.EntityFrameworkCore;
using Notes.API.Security.Models;
using Notes.API.Shared.Domain.Repositories;
using Notes.API.Shared.Persistence.Context;

namespace Notes.API.Shared.Persistence.Repositories;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
{
    protected readonly AppDbContext AppDbContext;
    protected readonly DbSet<TEntity> EntityDbSet;

    public BaseRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
        EntityDbSet = appDbContext.Set<TEntity>(); //Import to find a properly DbSet to do the job!
    }
    
    public async Task<IEnumerable<TEntity?>> ListAllAsync()
    {
        return await EntityDbSet.ToListAsync();
    }

    public async Task<TEntity?> FindAsync(TKey id)
    {
        return await EntityDbSet.FindAsync(id);
    }

    public async Task AddAsync(TEntity entity)
    {
        await EntityDbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        EntityDbSet.Update(entity);
    }

    public bool Exist(TKey id)
    {
        if (EntityDbSet.Find(id) == null)
            return true;
        return false;
    }

    public void Remove(TEntity entity)
    {
        EntityDbSet.Remove(entity);
    }
}