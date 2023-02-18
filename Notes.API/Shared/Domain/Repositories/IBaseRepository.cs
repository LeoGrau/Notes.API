namespace Notes.API.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity, TKey> where TEntity : class
{
    Task<IEnumerable<TEntity?>> ListAllAsync();
    Task<TEntity?> FindAsync(TKey id);
    Task AddAsync(TEntity newEntityData);
    void Update(TEntity updatedEntityData);
    void Remove(TEntity toDeletentityData);
}