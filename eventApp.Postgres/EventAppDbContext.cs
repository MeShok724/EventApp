using Microsoft.EntityFrameworkCore;
using EventApp.Core.Models;

namespace EventApp.Postgres
{
    public class EventAppDbContext(DbContextOptions<EventAppDbContext> options) : DbContext(options)
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }
    }
}
