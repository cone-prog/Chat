using AutoMapper;
using Chat.Api.Models;

namespace Chat.Api.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Domain.Entities.Conversation, Conversation>();
    }
}
