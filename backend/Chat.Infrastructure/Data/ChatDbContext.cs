using Chat.Domain.Data.DbContext;
using Chat.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Data;

public class ChatDbContext : DbContext, IChatDbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    DbSet<ChatParticipant> ChatParticipants => Set<ChatParticipant>();
    DbSet<Conversation> Conversations => Set<Conversation>();
    DbSet<Message> Messages => Set<Message>();
    DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
