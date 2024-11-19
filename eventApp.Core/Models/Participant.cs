using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Core.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate {  get; set; }
        public string Email { get; set; } = string.Empty;
        public Guid EventId { get; set; }

        public Event Event { get; set; } = new Event();
    }
}
