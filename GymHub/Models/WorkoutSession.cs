namespace GymHub.Models
{
    public class WorkoutSession
    {
        public int Id { get; set; }
        public DateTime PerformedOn { get; set; }
        public string? Notes { get; set; }

        public string? UserId { get; set; }
        public Users? User { get; set; }

        public ICollection<WorkoutSet> Sets { get; set; } = new List<WorkoutSet>();
    }
}
