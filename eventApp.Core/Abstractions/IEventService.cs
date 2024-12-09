using EventApp.Core.Models;

namespace eventApp.Application.Services
{
    public interface IEventService
    {
        Task<Guid> AddEvent(Event @event);
        Task<Guid> DeleteEvent(Guid id);
        Task<List<Event>> GetAllEvents();
        Task<Event?> GetEventByCategory(string category);
        Task<Event?> GetEventByDate(DateTime date);
        Task<Event?> GetEventById(Guid id);
        Task<Event?> GetEventByLocation(string location);
        Task<Event?> GetEventByName(string name);
        Task<Guid> UpdateEvent(Event @event);
    }
}