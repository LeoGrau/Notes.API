using AutoMapper;
using Notes.API.Security.Domain.Services.Communication.Responses;
using Notes.API.Security.Models;
using Notes.API.Security.Resources.Show;

namespace Notes.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, UserResource>();
        CreateMap<User, AuthResponse>();
    }
}