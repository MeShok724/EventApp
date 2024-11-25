

using EventApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventApp.Postgres.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<EventRepository>
    {
        public void Configure(EntityTypeBuilder<EventRepository> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Participants)
                .WithMany(p => p.Events);
        }
    }

    public class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantRepository>
    {
        public void Configure(EntityTypeBuilder<ParticipantRepository> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(p => p.Events)
                .WithMany(e => e.Participants);
        }
    }
}
