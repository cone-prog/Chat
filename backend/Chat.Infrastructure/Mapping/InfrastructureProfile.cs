using AutoMapper;
using Chat.Domain.UseCase.Users.SignIn;
using Chat.Infrastructure.Models;

namespace Chat.Infrastructure.Mapping;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Conversation, Domain.Entities.Conversation>();
        CreateMap<Message, Domain.Entities.Message>();
        CreateMap<User, RecognisedUser>();
        CreateMap<Session, Domain.Authentication.Session>();
    }
}
