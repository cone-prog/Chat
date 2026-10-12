using Chat.Api.Models;
using Chat.Domain.UseCase.Users.SignOn;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateUser(
        [FromBody] SignOn request,
        [FromServices] ISignOnUseCase useCase, 
        CancellationToken cancellationToken
    )
    {
        var command = new SignOnCommand(request.Name, request.Email, request.Password);
        var result = await useCase.Execute(command, cancellationToken);
        return Ok(result);
    }
}