using Chat.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    internal DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
    internal DbSet<Conversation> Conversations => Set<Conversation>();
    internal DbSet<Message> Messages => Set<Message>();
    internal DbSet<User> Users => Set<User>();
    internal DbSet<Session> Sessions => Set<Session>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
