using Microsoft.EntityFrameworkCore;
using EventApp.Core.Models;

namespace EventApp.Postgres
{
    public class EventAppDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ParticipantEntity> Participants { get; set; }

        public EventAppDbContext ()
    }
}
