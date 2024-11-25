using EventApp.Core.Models;
using EventApp.Postgres;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventApp.Postgres.Repositories
{
    public class EventRepository(EventAppDbContext context)
    {
        private readonly EventAppDbContext _context = context;

        public async Task<List<Event>> Get()
        {
            var dbResp = await _context.Events
                .ToListAsync();
            List<Event> list = dbResp.Select(e => Event.Create(e.Id, e.Name, e.Description, 
                e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image).Item1)
                .ToList();
            return list;
        }
    }
}
