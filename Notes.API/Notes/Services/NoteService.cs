using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Domain.Repositories;
using Notes.API.Notes.Domain.Services;
using Notes.API.Notes.Services.Communication;
using Notes.API.Shared.Domain.Repositories;

namespace Notes.API.Notes.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NoteService(INoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Note>> ListAllNotesAsync()
    {
        return await _noteRepository.ListAllAsync();
    }

    public async Task<IEnumerable<Note>> ListAllArchivedNotesAsync()
    {
        return await _noteRepository.ListAllArchivedAsync();
    }
    
    public async Task<IEnumerable<Note>> ListAllNotArchivedNotesAsync()
    {
        return await _noteRepository.ListAllNotArchivedAsync();
    }

    public async Task<NoteResponse> FindAsync(long noteId)
    {
        var existingNote = await _noteRepository.FindAsync(noteId);
        if (existingNote == null)
            return new NoteResponse("Sorry, this note does not exist!");
        return new NoteResponse(existingNote);
    }

    public async Task<NoteResponse> AddAsync(Note newNote)
    {
        try
        {
            newNote.SetLastEditedToNow(); //Set last edit date
            newNote.Archived = false; //Set default value as not archived.

            await _noteRepository.AddAsync(newNote);
            await _unitOfWork.CompleteAsync();
            return new NoteResponse(newNote);
        }
        catch (Exception exception)
        {
            return new NoteResponse($"{exception.Message}");
        }
    }

    public async Task<NoteResponse> UpdateAsync(long noteId, Note updateNote)
    {
        var existingNote = await _noteRepository.FindAsync(noteId);
        if (existingNote == null)
            return new NoteResponse("Sorry, this note does not exist!");

        //Updating
        existingNote.SetNote(updateNote);

        try
        {
            _noteRepository.Update(existingNote);
            await _unitOfWork.CompleteAsync();
            return new NoteResponse(existingNote);
        }
        catch (Exception exception)
        {
            return new NoteResponse($"{exception.Message}");
        }
    }

    public async Task<NoteResponse> RemoveAsync(long noteId)
    {
        var existingNote = await _noteRepository.FindAsync(noteId);
        if (existingNote == null)
            return new NoteResponse("Sorry, this note does not exist!");
        try
        {
            _noteRepository.Remove(existingNote);
            await _unitOfWork.CompleteAsync();
            return new NoteResponse(existingNote);
        }
        catch (Exception exception)
        {
            return new NoteResponse($"{exception.Message}");
        }
    }

    public async Task<Boolean> ExistAsync(long noteId)
    {
        return _noteRepository.Exist(noteId);
    }

    public async Task<NoteResponse> ArchiveNoteAsync(long noteId, bool status)
    {
        var existingNote = await _noteRepository.FindAsync(noteId);
        if (existingNote == null)
            return new NoteResponse("Sorry, this note does not exist!");
        
        //Archiving Note
        existingNote.SetArchivedStatus(status);

        try
        { 
            _noteRepository.Update(existingNote);
            await _unitOfWork.CompleteAsync();
            return new NoteResponse(existingNote);
        }
        catch (Exception exception)
        {
            return new NoteResponse($"{exception.Message}");
        }
    }
}