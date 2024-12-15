using eventApp.Postgres.Repositories;
using EventApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Application.Services
{
    public class EventService(IEventRepository eventRepository) : IEventService
    {
        private readonly IEventRepository _eventRepository = eventRepository;

        public async Task<List<Event>> GetAllEvents()
        {
            return await _eventRepository.Get();
        }
        public async Task<Event?> GetEventById(Guid id)
        {
            return await _eventRepository.GetById(id);
        }
        public async Task<Event?> GetEventByName(string name)
        {
            return await _eventRepository.GetByName(name);
        }
        public async Task<Event?> GetEventByDate(DateTime date)
        {
            return await _eventRepository.GetByDate(date);
        }
        public async Task<Event?> GetEventByLocation(string location)
        {
            return await _eventRepository.GetByLocation(location);
        }
        public async Task<List<Event>> GetEventByCategory(string category)
        {
            return await _eventRepository.GetByCategory(category);
        }
        public async Task<Guid> AddEvent(Event @event)
        {
            return await _eventRepository.Add(@event);
        }
        public async Task<Guid> UpdateEvent(Event @event)
        {
            return await _eventRepository.Update(@event);
        }
        public async Task<Guid> DeleteEvent(Guid id)
        {
            return await _eventRepository.Delete(id);
        }
    }
}
