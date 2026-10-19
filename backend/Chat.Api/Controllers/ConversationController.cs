using AutoMapper;
using Chat.Api.Models;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Chat.Domain.UseCase.Conversations.GetConversations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("conversations")]
public class ConversationController(IMapper mapper, IMediator   mediator) : ControllerBase
{
    [HttpGet("{conversationId}", Name = "GetById")]
    [ProducesResponseType(200)]
    [ProducesResponseType(403)]
    [ProducesResponseType(410)]
    public async Task<ActionResult<Conversation>> GetById(
        Guid conversationId,
        CancellationToken cancellationToken)
    {
        var conversation = await mediator.Send(new GetConversationByIdQuery(conversationId), cancellationToken);
        return Ok(mapper.Map<Conversation>(conversation));
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateConversation(
        [FromBody] CreateConversation createConversation,
        CancellationToken cancellationToken)
    {
        var command = new CreateConversationCommand(createConversation.Name);
        var conversation = await mediator.Send(command,
            cancellationToken);
        return CreatedAtRoute(nameof(GetById), new { conversationId = conversation.Id },
            mapper.Map<Conversation>(conversation));
    }

    [HttpGet]
    public async Task<ActionResult<List<Conversation>>> GetConversation(
        CancellationToken cancellationToken)
    {
        var conversations = await mediator.Send(new GetConversationsQuery(),cancellationToken);
        return Ok(conversations.Select(c => mapper.Map<Conversation>(c)));
    }
}
