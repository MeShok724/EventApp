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
        public async Task<ActionResult<List<EventResponse>>> GetAll()
        {
            var events = await _eventService.GetAllEvents();
            var resp = events.Select(e => new EventResponse(e.Id, e.Name, e.Description, e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image))
                .ToList();
            return Ok(resp);
        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<EventResponse?>> GetById(Guid id)
        {
            var @event = await _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description, 
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<EventResponse?>> GetByName(string name)
        {
            var @event = await _eventService.GetEventByName(name);
            if (@event == null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("{date:datetime}")]
        public async Task<ActionResult<EventResponse?>> GetByDate(DateTime date)
        {
            var @event = await _eventService.GetEventByDate(date);
            if (@event == null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("location/{location}")]
        public async Task<ActionResult<EventResponse?>> GetByLocation(string location)
        {
            var @event = await _eventService.GetEventByLocation(location);
            if (@event == null)
            {
                return NotFound();
            }
            EventResponse eventResponse = new EventResponse(@event.Id, @event.Name, @event.Description,
                @event.DateTime, @event.Location, @event.Category, @event.MaxParticipants, @event.Image);
            return Ok(eventResponse);
        }
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<EventResponse>>> GetByCategory(string category)
        {
            var @events = await _eventService.GetEventByCategory(category);
            if (@events == null)
            {
                return NotFound();
            }
            var resp = events.Select(e => new EventResponse(e.Id, e.Name, e.Description,
                e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image))
                .ToList();
            return Ok(resp);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] EventRequest eventRequest)
        {
            var createRes = Event.Create(Guid.NewGuid(), eventRequest.Name, eventRequest.Description,
                eventRequest.DateTime, eventRequest.Location, eventRequest.Category, eventRequest.MaxParticipants, eventRequest.Image);
            if (!string.IsNullOrEmpty(createRes.Item2)) {
                return BadRequest(createRes.Item2);    
            }
            Event @event = createRes.Item1;
            Guid addToDbRes = await _eventService.AddEvent(@event);
            if (addToDbRes == Guid.Empty)
            {
                return BadRequest("Failed to add event to database");
            }
            return Ok(addToDbRes);
        }
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] EventRequest eventRequest)
        {
            var createRes = Event.Create(id, eventRequest.Name, eventRequest.Description,
                eventRequest.DateTime, eventRequest.Location, eventRequest.Category, eventRequest.MaxParticipants, eventRequest.Image);
            if (!string.IsNullOrEmpty(createRes.Item2))
            {
                return BadRequest(createRes.Item2);
            }
            Event @event = createRes.Item1;
            Guid res = await _eventService.UpdateEvent(@event);
            return Ok(res);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            return await _eventService.DeleteEvent(id);
        }
    }
}
