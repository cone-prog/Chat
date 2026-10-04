using Chat.Domain.Data.DbContext;
using Chat.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data;

public class ChatDbContext : DbContext, IChatDbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    internal DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
    internal DbSet<Conversation> Conversations => Set<Conversation>();
    internal DbSet<Message> Messages => Set<Message>();
    internal DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
