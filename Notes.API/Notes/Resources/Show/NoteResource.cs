namespace Notes.API.Notes.Resources.Show;

public class NoteResource
{
    public long NoteId { get; set; }
    public String Title { get; set; }
    public String Content { get; set; }
    public DateTime LastTimeEdited { get; set; }
    public Boolean Archived { get; set; }
}