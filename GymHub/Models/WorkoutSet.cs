using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymHub.Models
{
    public class WorkoutSet
    {
        public int Id { get; set; }
        public int Reps { get; set; }
        public double Weight { get; set; }
        [Required]
        public int ExerciseId { get; set; }

        [ValidateNever]
        public Exercises Exercise { get; set; }
        [Required]
        public int WorkoutSessionId { get; set; }

        [ValidateNever]
        public WorkoutSession WorkoutSession { get; set; }
    }
}
