using EventApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Core.Models
{
    public class ParticipantEvent
    {
        public Guid ParticipantId { get; }
        public Guid EventId { get; }
        public DateTime RegistrationTime { get; }

        public Participant Participant { get; }
        public Event Event { get; }

        public ParticipantEvent(Guid participantId, Guid eventId, DateTime registrationTime)
        {
            ParticipantId = participantId;
            EventId = eventId;
            RegistrationTime = registrationTime;
        }
    }
}
