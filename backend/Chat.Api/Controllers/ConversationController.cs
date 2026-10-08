using AutoMapper;
using Chat.Api.Models;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("conversations")]
public class ConversationController(IMapper mapper) : ControllerBase
{
    [HttpGet("{conversationId}", Name = "GetById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(410)]
    public async Task<ActionResult<Conversation>> GetById(
        Guid conversationId,
        [FromServices] IGetConversationByIdUseCase useCase,
        CancellationToken cancellationToken)
    {
        var conversation = await useCase.Execute(conversationId, cancellationToken);
        return Ok(mapper.Map<Conversation>(conversation));
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateConversation(
        [FromServices] ICreateConversationUseCase useCase,
        [FromBody] CreateConversation createConversation,
        CancellationToken cancellationToken)
    {
        var command = new CreateConversationCommand(createConversation.Name);
        var conversation = await useCase.Execute(command,
            cancellationToken);
        return CreatedAtRoute(nameof(GetById), new { conversationId = conversation.Id },
            mapper.Map<Conversation>(conversation));
    }
}
