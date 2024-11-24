
namespace EventApp.Core.Models
{
    public class Event
    {
        public const int MAX_NAME_LENGTH = 100;
        public const int MAX_DESCRIPTION_LENGTH = 250;
        public const int MAX_LOCATION_LENGTH = 250;
        public const int MAX_CATEGORY_LENGTH = 100;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public byte[] Image { get; set; } = [];

        public List<Participant> Participants { get; set; } = [];

        public static (Event, string) Create(Guid Id, string Name, string Description, DateTime DateTime,
            string Location, string Category, int MaxParticipants, byte[] Image)
        {
            string error = string.Empty;

            if (string.IsNullOrEmpty(Name) || Name.Length > MAX_NAME_LENGTH) {
                error = "Name can not be empty or longer than 100 symbols";
            } else if (string.IsNullOrEmpty(Description) || Description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error = "Description can not be empty or longer than 250 symbols";
            } else if (string.IsNullOrEmpty(Location) || Location.Length > MAX_LOCATION_LENGTH)
            {
                error = "Location can not be empty or longer than 250 symbols";
            } else if (string.IsNullOrEmpty(Category) || Category.Length > MAX_CATEGORY_LENGTH)
            {
                error = "Category can not be empty or longer than 100 symbols";
            }

            Event Event = new Event(Id, Name, Description, DateTime, Location, Category, MaxParticipants, Image);
            return (Event,  error);
        }

        private Event(Guid Id, string Name, string Description, DateTime DateTime, string Location,
            string Category, int MaxParticipants, byte[] Image)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.DateTime = DateTime;
            this.Location = Location;
            this.Category = Category;
            this.MaxParticipants = MaxParticipants;
            this.Image = Image;
        }
    }
}
