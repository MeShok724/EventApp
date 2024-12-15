using EventApp.Core.Models;
using EventApp.Postgres;
using Microsoft.EntityFrameworkCore;

namespace eventApp.Postgres.Repositories
{
    public class EventRepository(EventAppDbContext context) : IEventRepository
    {
        private readonly EventAppDbContext _context = context;

        public async Task<List<Event>> Get()
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .ToListAsync();
            List<Event> list = dbResp.Select(e => Event.Create(e.Id, e.Name, e.Description,
                e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image).Item1)
                .ToList();
            return list;
        }
        public async Task<Event?> GetById(Guid id)
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
            if (dbResp == null)
                return null;
            var resp = Event.Create(dbResp.Id, dbResp.Name, dbResp.Description,
                dbResp.DateTime, dbResp.Location, dbResp.Category, dbResp.MaxParticipants, dbResp.Image);
            return resp.Item1;
        }
        public async Task<Event?> GetByName(string name)
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Name == name);
            if (dbResp == null)
                return null;
            var resp = Event.Create(dbResp.Id, dbResp.Name, dbResp.Description,
                dbResp.DateTime, dbResp.Location, dbResp.Category, dbResp.MaxParticipants, dbResp.Image);
            return resp.Item1;
        }
        public async Task<Event?> GetByDate(DateTime date)
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.DateTime == date);
            if (dbResp == null)
                return null;
            var resp = Event.Create(dbResp.Id, dbResp.Name, dbResp.Description,
                dbResp.DateTime, dbResp.Location, dbResp.Category, dbResp.MaxParticipants, dbResp.Image);
            return resp.Item1;
        }
        public async Task<Event?> GetByLocation(string location)
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Location == location);
            if (dbResp == null)
                return null;
            var resp = Event.Create(dbResp.Id, dbResp.Name, dbResp.Description,
                dbResp.DateTime, dbResp.Location, dbResp.Category, dbResp.MaxParticipants, dbResp.Image);
            return resp.Item1;
        }
        public async Task<List<Event>> GetByCategory(string category)
        {
            var dbResp = await _context.Events
                .AsNoTracking()
                .Where(e => e.Category == category)
                .ToListAsync();
            if (dbResp == null)
                return null;
            var resp = dbResp.Select(e => Event.Create(e.Id, e.Name, e.Description,
                e.DateTime, e.Location, e.Category, e.MaxParticipants, e.Image).Item1)
                .ToList();
            return resp;
        }
        public async Task<Guid> Add(Event @event)
        {
            EventEntity dbReq = new EventEntity();
            dbReq.Id = @event.Id;
            dbReq.Name = @event.Name;
            dbReq.Description = @event.Description;
            dbReq.DateTime = @event.DateTime;
            dbReq.Location = @event.Location;
            dbReq.Category = @event.Category;
            dbReq.MaxParticipants = @event.MaxParticipants;
            dbReq.Image = @event.Image;

            await _context.Events
                .AddAsync(dbReq);
            return @event.Id;
        }
        public async Task<Guid> Update(Event @event)
        {
            EventEntity dbReq = new EventEntity();
            dbReq.Id = @event.Id;
            dbReq.Name = @event.Name;
            dbReq.Description = @event.Description;
            dbReq.DateTime = @event.DateTime;
            dbReq.Location = @event.Location;
            dbReq.Category = @event.Category;
            dbReq.MaxParticipants = @event.MaxParticipants;
            dbReq.Image = @event.Image;

            await _context.Events
                .Where(e => e.Id == @event.Id)
                .ExecuteUpdateAsync(e => e
                    .SetProperty(e => e.Name, @event.Name)
                    .SetProperty(e => e.Description, @event.Description)
                    .SetProperty(e => e.DateTime, @event.DateTime)
                    .SetProperty(e => e.Location, @event.Location)
                    .SetProperty(e => e.Category, @event.Category)
                    .SetProperty(e => e.MaxParticipants, @event.MaxParticipants)
                    .SetProperty(e => e.Image, @event.Image)
                    );
            return @event.Id;
        }
        public async Task<Guid> Delete(Guid id)
        {
            var dbResp = await _context.Events
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
