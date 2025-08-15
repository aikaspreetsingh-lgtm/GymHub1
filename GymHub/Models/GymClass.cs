namespace GymHub.Models
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public int Capacity { get; set; }

        public ICollection<ClassBooking> Bookings { get; set; } = new List<ClassBooking>();
    }
}

