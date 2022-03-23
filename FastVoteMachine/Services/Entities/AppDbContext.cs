using Microsoft.EntityFrameworkCore;

namespace FastVoteMachine.Services.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<VoteEntity> Votes { get; set; }

    public DbSet<ConnectionEntity> Connections { get; set; }
}