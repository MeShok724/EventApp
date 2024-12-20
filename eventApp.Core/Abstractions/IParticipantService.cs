﻿using EventApp.Core.Models;

namespace eventApp.Application.Services
{
    public interface IParticipantService
    {
        Task<Guid?> AddParticipant(Participant participant);
        Task<Guid> DeleteParticipantEvent(Guid participantId, Guid eventId);
        Task<List<Participant>> GetAllParticipants();
        Task<Participant?> GetParticipantById(Guid id);
        Task<string> AddParticipantEvent(Guid participantId, Guid eventId);
        Task<List<Participant>> GetParticipantsOfEvent(Guid eventId);
    }
}