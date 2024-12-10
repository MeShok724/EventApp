using eventApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace eventApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;
    }
}
