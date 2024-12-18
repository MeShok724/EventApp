using eventApp.Postgres.Repositories;
using EventApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Application.Services
{
    public class ParticipantService(IParticipantRepository participantRepository) : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository = participantRepository;

        public async Task<List<Participant>> GetAllParticipants()
        {
            return await _participantRepository.GetAll();
        }
        public async Task<Participant?> GetParticipantById(Guid id)
        {
            return await _participantRepository.GetById(id);
        }
        public async Task<Guid?> AddParticipant(Participant participant)
        {
            return await _participantRepository.Add(participant);
        }
        public async Task<string> AddParticipantEvent(Guid participantId, Guid eventId)
        {
            return await _participantRepository.AddEvent(participantId, eventId);
        }
        public async Task<Guid> DeleteParticipantEvent(Guid participantId, Guid eventId)
        {
            return await _participantRepository.DeleteEvent(participantId, eventId);
        }
    }
}
