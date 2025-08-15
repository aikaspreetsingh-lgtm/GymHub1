namespace GymHub.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DurationWeeks { get; set; }
        public string DifficultyLevel { get; set; }
        public string Goal { get; set; }

        public bool IsTemplate { get; set; } = true;

        // If assigned to a user
        public string? UserId { get; set; }
        public Users? User{ get; set; }
    }
}
