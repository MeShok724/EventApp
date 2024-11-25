using EventApp.Postgres;
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
    }
}
