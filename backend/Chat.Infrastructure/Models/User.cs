using System.ComponentModel.DataAnnotations;

namespace Chat.Infrastructure.Models;

internal class User
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    [MaxLength(16)]
    public byte[] Salt { get; set; } = null!;

    [MaxLength(32)]
    public byte[] PasswordHash { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
