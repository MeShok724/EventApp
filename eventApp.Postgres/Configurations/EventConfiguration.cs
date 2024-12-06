

using EventApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventApp.Postgres.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Participants)
                .WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "ParticipantEvent",
                    j => j.HasOne<ParticipantEntity>().WithMany().HasForeignKey("ParticipantId"),
                    j => j.HasOne<EventEntity>().WithMany().HasForeignKey("EventId"));
        }
    }

    public class ParticipantConfiguration : IEntityTypeConfiguration<ParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipantEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(p => p.Events)
                .WithMany(e => e.Participants);
        }
    }
}
