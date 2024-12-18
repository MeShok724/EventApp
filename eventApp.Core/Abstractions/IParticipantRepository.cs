using EventApp.Core.Models;

namespace eventApp.Postgres.Repositories
{
    public interface IParticipantRepository
    {
        Task<Guid?> Add(Participant participant);
        Task<string> AddEvent(Guid participantId, Guid eventId);
        Task<Guid> DeleteEvent(Guid participantId, Guid eventId);
        Task<List<Participant>> GetAll();
        Task<Participant?> GetById(Guid id);
    }
}