using Notes.API.Notes.Domain.Models;
using Notes.API.Shared.Domain.Services.Communication;

namespace Notes.API.Notes.Services.Communication;

public class NoteResponse : BaseResponse<Note>
{
    public NoteResponse(string? message) : base(message)
    {
    }

    public NoteResponse(Note? resource) : base(resource)
    {
    }
}