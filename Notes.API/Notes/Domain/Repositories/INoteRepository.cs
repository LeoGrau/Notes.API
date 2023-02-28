using Notes.API.Notes.Domain.Models;
using Notes.API.Shared.Domain.Repositories;

namespace Notes.API.Notes.Domain.Repositories;

public interface INoteRepository : IBaseRepository<Note, long>
{
    Task<IEnumerable<Note>> ListAllArchivedAsync();
    Task<IEnumerable<Note>> ListAllNotArchivedAsync();

}