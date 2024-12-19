using eventApp.API.Contracts;
using eventApp.Application.Services;
using EventApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace eventApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipantController(IParticipantService participantService) : Controller
    {
        private readonly IParticipantService _participantService = participantService;
        [HttpGet]
        public async Task<ActionResult<List<ParticipantResponse>>> GetAll()
        {
            var respFromDb = await _participantService.GetAllParticipants();
            List<ParticipantResponse> respToClient = respFromDb
                .Select(p => new ParticipantResponse(p.Id, p.FirstName, p.LastName, p.DateOfBirth, p.Email))
                .ToList();
            return Ok(respToClient);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ParticipantResponse?>> GetById(Guid id)
        {
            var respFromDb = await _participantService.GetParticipantById(id);
            if (respFromDb == null)
                return NotFound();
            ParticipantResponse respToClient = new ParticipantResponse(respFromDb.Id,
                respFromDb.FirstName, respFromDb.LastName, respFromDb.DateOfBirth, respFromDb.Email);
            return Ok(respToClient);
        }
        [HttpGet("/byEvent/{eventId:guid}")]
        public async Task<ActionResult<ParticipantResponse>> GetOfEvent(Guid eventId)
        {
            var respFromDb = await _participantService.GetParticipantsOfEvent(eventId);
            List<ParticipantResponse> respToClient = respFromDb
                .Select(p => new ParticipantResponse(p.Id, p.FirstName, p.LastName, p.DateOfBirth, p.Email))
                .ToList();
            return Ok(respToClient);
        }
        [HttpPost]
        public async Task<ActionResult<Guid?>> Add([FromBody] ParticipantRequest participantRequest)
        {
            var createResult = Participant.Create(Guid.NewGuid(), participantRequest.FirstName, participantRequest.LastName,
                participantRequest.DateOfBirth, participantRequest.Email);
            if (!string.IsNullOrEmpty(createResult.Item2))
            {
                return BadRequest(createResult.Item2);
            }
            Participant participant = createResult.Item1;
            var dbResp = await _participantService.AddParticipant(participant);
            if (dbResp == Guid.Empty) 
            {
                return BadRequest("Failed to add participant to database");
            }
            return Ok(dbResp);
        }

        [HttpPost("{eventId:guid}/{participantId:guid}")]
        public async Task<ActionResult<string>> AddParticipantEvent(Guid participantId, Guid eventId)
        {
            var resp = await _participantService.AddParticipantEvent(participantId, eventId);
            if (!string.IsNullOrEmpty(resp))
            {
                return BadRequest(resp);
            }
            return Ok();
        }

        [HttpDelete("{eventId:guid}/{participantId:guid}")]
        public async Task<ActionResult<Guid>> DeleteParticipantEvent(Guid participantId, Guid eventId)
        {
            return await _participantService.DeleteParticipantEvent(participantId, eventId);
        }
    }
}
