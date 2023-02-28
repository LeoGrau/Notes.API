using AutoMapper;
using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Resources.Show;

namespace Notes.API.Notes.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Note, NoteResource>();
    }
}