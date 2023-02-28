using AutoMapper;
using Notes.API.Notes.Domain.Models;
using Notes.API.Notes.Resources.Create;
using Notes.API.Notes.Resources.Show;
using Notes.API.Notes.Resources.Update;

namespace Notes.API.Notes.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<UpdateNoteResource, Note>();
        CreateMap<CreateNoteResource, Note>();
    }
}