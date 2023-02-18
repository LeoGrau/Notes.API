using Notes.API.Shared.Domain.Repositories;
using Notes.API.Shared.Persistence.Context;

namespace Notes.API.Shared.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task CompleteAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}