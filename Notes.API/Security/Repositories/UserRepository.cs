using Notes.API.Security.Domain.Repositories;
using Notes.API.Security.Models;
using Notes.API.Shared.Persistence.Context;
using Notes.API.Shared.Persistence.Repositories;

namespace Notes.API.Security.Repositories;

public class UserRepository : BaseRepository<User, long>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}