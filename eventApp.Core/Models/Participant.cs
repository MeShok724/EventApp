using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Core.Models
{
    public class Participant
    {
        private const int NAME_NAX_LENGTH = 100;
        private const int EMAIL_NAX_LENGTH = 100;
        public Guid Id { get; }
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public DateTime DateOfBirth { get; }
        public string Email { get; } = string.Empty;

        public List<Event> Events { get; } = [];

        private Participant(Guid id, string firstName, string lastName, DateTime dateOfBirth, string email) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        public static (Participant, string) Create(Guid id, string firstName, string lastName, DateTime dateOfBirth, string email)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(firstName) || firstName.Length > NAME_NAX_LENGTH) {
                error = "First name can not be empty or longer than 100 symbols";
            } 
            else if (string.IsNullOrEmpty(lastName) || lastName.Length > NAME_NAX_LENGTH)
            {
                error = "Last name can not be empty or longer than 100 symbols";
            }
            else if (string.IsNullOrEmpty(email) || email.Length > NAME_NAX_LENGTH)
            {
                error = "Email can not be empty or longer than 100 symbols";
            }

            Participant participant = new Participant(id, firstName, lastName, dateOfBirth, email);
            return (participant, error);
        }
    }
}
