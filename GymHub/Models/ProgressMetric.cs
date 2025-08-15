namespace GymHub.Models
{
    public class ProgressMetric
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }
        public double WeightKg { get; set; }
        public double BodyFatPercent { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }
    }
}
