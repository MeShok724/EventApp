using Microsoft.EntityFrameworkCore;
using EventApp.Core.Models;

namespace EventApp.Postgres
{
    public class EventAppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        public EventAppDbContext ()
    }
}
