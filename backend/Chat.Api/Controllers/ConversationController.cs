using Chat.Api.Models;
using Chat.Domain.UseCase.Conversations.CreateConversation;
using Chat.Domain.UseCase.Conversations.GetConversationById;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("conversations")]
public class ConversationController : ControllerBase
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
        return Ok(new Conversation
        {
            Id = conversation.Id,
            Name = conversation.Name,
            CreatedAt = conversation.CreatedAt
        });
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> CreateConversation(
        [FromServices] ICreateConversationUseCase useCase,
        [FromBody] CreateConversation createConversation,
        CancellationToken cancellationToken)
    {
        var conversation = await useCase.Execute(createConversation.Name,
            cancellationToken);
        return CreatedAtRoute(nameof(GetById), new { conversationId = conversation.Id }, new Conversation
        {
            Id = conversation.Id,
            Name = conversation.Name,
            CreatedAt = conversation.CreatedAt
        });
    }
}
