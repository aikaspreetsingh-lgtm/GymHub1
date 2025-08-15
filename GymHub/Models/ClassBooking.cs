using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GymHub.Models
{
    public class ClassBooking
    {
        public int Id { get; set; }
        public DateTime BookedAt { get; set; }

        public string UserId { get; set; }


        [ValidateNever]
        public Users User { get; set; }


        [ValidateNever]
        public int GymClassId { get; set; }
        public GymClass GymClass { get; set; }
    }
}
