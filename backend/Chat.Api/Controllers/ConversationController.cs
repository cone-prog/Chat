using Chat.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConversationController : ControllerBase
{
    public async Task<ActionResult<ConversationDto>> GetConversationById(Guid conversationId,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult> CreateConversation(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
