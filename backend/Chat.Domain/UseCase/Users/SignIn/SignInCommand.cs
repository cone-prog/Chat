namespace Chat.Domain.UseCase.Users.SignIn;

public record SignInCommand(
    string Email,
    string Password);
