using eventApp.API.Contracts;
using eventApp.Application.Services;
using EventApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
            var resp = events.Select(e => new EventResponse(e.Id, e.Name, e.Description, e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image));
            return Ok(resp);
        }

    }
}
