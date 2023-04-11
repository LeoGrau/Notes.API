using Microsoft.EntityFrameworkCore;
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

    public bool ExistByEmail(String email)
    {
        return AppDbContext.Users!.Any(user => user.Email == email);
    }

    public async Task<User?> FindByEmailAsync(String email)
    {
        return await AppDbContext.Users!.FirstOrDefaultAsync(user => user.Email == email);
    }
}