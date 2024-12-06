﻿using EventApp.Core.Models;
using EventApp.Postgres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Postgres.Repositories
{
    public class ParticipantRepository(EventAppDbContext context)
    {
        private readonly EventAppDbContext _context = context;
        public async Task<List<Participant>> GetAll()
        {
            var resp = await _context.Participants
                .AsNoTracking()
                .ToListAsync();
            return resp
                .Select(pE => Participant.Create(pE.Id, pE.FirstName, pE.LastName, pE.DateOfBirth, pE.Email).Item1)
                .ToList();
        }
        public async Task<Participant?> GetById(Guid id)
        {
            var resp = await _context.Participants
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (resp == null)
                return null;
            return Participant.Create(resp.Id, resp.FirstName, resp.LastName, resp.DateOfBirth, resp.Email).Item1;
        }
        public async Task<Guid?> Add(Participant participant)
        {
            if (participant == null)
                return null;
            ParticipantEntity participantEntity = new ParticipantEntity
            {
                Id = participant.Id,
                FirstName = participant.FirstName,
                LastName = participant.LastName,
                DateOfBirth = participant.DateOfBirth,
                Email = participant.Email
            };

            var resp = await _context.Participants
                .AddAsync(participantEntity);
            await _context.SaveChangesAsync();
            return participantEntity.Id;
        }
        public async Task<Guid> DeleteEvent(Guid participantId, Guid eventId)
        {
            await _context.ParticipantEvents
                .Where(pE => pE.EventId == eventId && pE.ParticipantId == participantId)
                .ExecuteDeleteAsync();
            return participantId;
        }
    }
}
