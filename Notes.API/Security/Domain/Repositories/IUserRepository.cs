using Notes.API.Security.Models;
using Notes.API.Shared.Domain.Repositories;

namespace Notes.API.Security.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User, long>
{
}