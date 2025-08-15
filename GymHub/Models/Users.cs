using Microsoft.AspNetCore.Identity;

namespace GymHub.Models
{
    public class Users: IdentityUser
    {
        public string FullName { get; set; }

        public ICollection<WorkoutSession> WorkoutSession { get; set; } = new List<WorkoutSession>();
        public ICollection<ProgressMetric> ProgressMetric { get; set; } = new List<ProgressMetric>();
        public ICollection<ClassBooking> ClassBookings { get; set; } = new List<ClassBooking>();
        public ICollection<NutritionPlan> NutritionPlans { get; set; } = new List<NutritionPlan>();
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
    }
}
