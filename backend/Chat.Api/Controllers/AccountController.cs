using Chat.Api.Authentication;
using Chat.Api.Models;
using Chat.Domain.UseCase.Users.SignIn;
using Chat.Domain.UseCase.Users.SignOn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateUser(
        [FromBody] SignOn request,
        CancellationToken cancellationToken
    )
    {
        var command = new SignOnCommand(request.Name, request.Email, request.Password);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("signIn")]
    public async Task<ActionResult> SignIn(
        [FromBody] SignIn request,
        [FromServices] IAuthTokenStorage storage,
        CancellationToken cancellationToken)
    {
        var command = new SignInCommand(request.Email, request.Password);
        var result = await mediator.Send(command, cancellationToken);
        storage.Store(HttpContext,result.token);
        return Ok(result);
    }
}
