using Microsoft.EntityFrameworkCore;
using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Domain.Repositories;
using Notes.API.Shared.Persistence.Context;
using Notes.API.Shared.Persistence.Repositories;

namespace Notes.API.Notes.Repositories;

public class NoteRepository : BaseRepository<Note, long>, INoteRepository
{
    public NoteRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<IEnumerable<Note>> ListAllArchivedAsync()
    {
        return await AppDbContext.Notes.Where(note => note.Archived).ToListAsync();
    }

    public async Task<IEnumerable<Note>> ListAllNotArchivedAsync()
    {
        return await AppDbContext.Notes.Where(note => !note.Archived).ToListAsync();
    }
}