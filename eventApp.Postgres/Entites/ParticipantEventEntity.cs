using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Postgres.Entites
{
    public class ParticipantEventEntity
    {
        public Guid ParticipantId { get; set; }
        public Guid EventId { get; set; }
        public DateTime RegistrationTime { get; set; }
    }
}
