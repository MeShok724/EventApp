using eventApp.API.Contracts;
using eventApp.Application.Services;
using EventApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eventApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpGet]
        public async Task<ActionResult<List<EventResponse>>> Get()
        {
            var events = await _eventService.GetAllEvents();
            var resp = events.Select(e => new EventResponse(e.Id, e.Name, e.Description, e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image))
                .ToList();
            return Ok(resp);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EventResponse?>> GetEventById(Guid id)
        {
            var @event = await _eventService.GetEventById(id);
            if (@event != null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description, 
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Event?>> GetEventByName(string name)
        {
            var @event = await _eventService.GetEventByName(name);
            if (@event != null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("{date:datetime}")]
        public async Task<ActionResult<Event?>> GetEventByDate(DateTime date)
        {
            var @event = await _eventService.GetEventByDate(date);
            if (@event != null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("location/{location}")]
        public async Task<ActionResult<Event?>> GetEventByLocation(string location)
        {
            var @event = await _eventService.GetEventByLocation(location);
            if (@event != null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<Event>>> GetEventByCategory(string category)
        {
            var @events = await _eventService.GetEventByCategory(category);
            if (@events != null)
            {
                return NotFound();
            }
            var resp = events.Select(e => new EventResponse(e.Id, e.Name, e.Description,
                e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image))
                .ToList();
            return Ok(resp);
        }
        public async Task<Guid> AddEvent(Event @event)
        {
            return await _eventRepository.Add(@event);
        }
        public async Task<Guid> UpdateEvent(Event @event)
        {
            return await UpdateEvent(@event);
        }
        public async Task<Guid> DeleteEvent(Guid id)
        {
            return await _eventRepository.Delete(id);
        }
    }
}
