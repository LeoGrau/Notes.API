namespace Notes.API.Notes.Domain.Models;

public class Note
{
    public long NoteId { get; set; }
    public String Title { get; set; }
    public String Content { get; set; }
    public DateTime LastTimeEdited { get; set; }
    public Boolean Archived { get; set; }

    public void SetNote(Note note)
    {
        Title = note.Title;
        Content = note.Content;
        LastTimeEdited = DateTime.Now;
    }

    public void SetLastEditedToNow()
    {
        LastTimeEdited = DateTime.Now;
    }

    public void SetArchivedStatus(bool status)
    {
        Archived = status;
    }
}