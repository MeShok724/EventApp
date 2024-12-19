using EventApp.Core.Models;
using EventApp.Postgres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Postgres.Repositories
{
    public class ParticipantRepository(EventAppDbContext context) : IParticipantRepository
    {
        private readonly EventAppDbContext _context = context;
        public async Task<List<Participant>> GetAll()
        {
            var resp = await _context.Participants
                .AsNoTracking()
                .ToListAsync();
            return resp
                .Select(pE => Participant.Create(pE.Id, pE.FirstName, pE.LastName, pE.DateOfBirth, pE.Email).Item1)
                .ToList();
        }
        public async Task<Participant?> GetById(Guid id)
        {
            var resp = await _context.Participants
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (resp == null)
                return null;
            return Participant.Create(resp.Id, resp.FirstName, resp.LastName, resp.DateOfBirth, resp.Email).Item1;
        }
        public async Task<Guid?> Add(Participant participant)
        {
            if (participant == null)
                return null;
            ParticipantEntity participantEntity = new ParticipantEntity
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                DateOfBirth = participant.DateOfBirth,
                Email = participant.Email
            };

            var resp = await _context.Participants
                .AddAsync(participantEntity);
            await _context.SaveChangesAsync();
            return participantEntity.Id;
        }

        public async Task<string> AddEvent(Guid participantId, Guid eventId)
        {
            var participant = await _context.Participants
                .Include(p => p.Events)
                .FirstOrDefaultAsync(p => p.Id == participantId);
            if (participant == null)
                return "User not found";
            var @event = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if (@event == null)
                return "Event not found";
            participant.Events.Add(@event);
            await _context.SaveChangesAsync();
            return string.Empty;
        }

        public async Task<Guid> DeleteEvent(Guid participantId, Guid eventId)
        {
            var participant = await _context.Participants
                .Include(p => p.Events)
                .FirstOrDefaultAsync(p => p.Id == participantId);
            if (participant == null)
                return Guid.Empty;
            var @event = participant.Events.FirstOrDefault(e => e.Id == eventId);
            if (@event == null)
                return Guid.Empty;
            participant.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return participantId;
        }
        public async Task<List<Participant>> GetParticipantsOfEvent(Guid eventId)
        {
            var @event = await _context.Events
                .Include(e => e.Participants)
                .FirstOrDefaultAsync(e => e.Id == eventId);
            if (@event == null)
                return [];
            var participantsEntity = @event.Participants.ToList();
            List<Participant> participants = participantsEntity
                .Select(pE => Participant.Create(pE.Id, pE.FirstName, pE.LastName, pE.DateOfBirth, pE.Email).Item1)
                .ToList();
            return participants;
        }
    }
}
