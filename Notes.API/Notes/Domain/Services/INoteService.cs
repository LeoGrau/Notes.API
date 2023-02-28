using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Services.Communication;

namespace Notes.API.Notes.Domain.Services;

public interface INoteService
{
    Task<IEnumerable<Note>> ListAllNotesAsync();
    Task<IEnumerable<Note>> ListAllNotArchivedNotesAsync();
    Task<IEnumerable<Note>> ListAllArchivedNotesAsync();
    Task<NoteResponse> FindAsync(long noteId);
    Task<NoteResponse> AddAsync(Note newNote);
    Task<NoteResponse> UpdateAsync(long noteId, Note updateNote);
    Task<NoteResponse> RemoveAsync(long noteId);
    Task<NoteResponse> ArchiveNoteAsync(long noteId, bool status);
    Task<bool> ExistAsync(long noteId);
}