using EventApp.Core.Models;

namespace eventApp.Postgres.Repositories
{
    public interface IEventRepository
    {
        Task<Guid> Add(Event @event);
        Task<Guid> Delete(Guid id);
        Task<List<Event>> Get();
        Task<Event?> GetByCategory(string category);
        Task<Event?> GetByDate(DateTime date);
        Task<Event?> GetById(Guid id);
        Task<Event?> GetByLocation(string location);
        Task<Event?> GetByName(string name);
        Task<Guid> Update(Event @event);
    }
}