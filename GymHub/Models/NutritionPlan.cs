namespace GymHub.Models
{
    public class NutritionPlan
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTemplate { get; set; } = true;
        public int TotalCaloriesPerDay { get; set; }   
        public int ProteinGrams { get; set; }         
        public int CarbsGrams { get; set; }           
        public int FatsGrams { get; set; }
        // If assigned to a user
        public string? UserId { get; set; }
        public Users? User { get; set; }
    }
}
