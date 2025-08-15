
using GymHub.Models;

namespace GymHub.ViewModels
{
    public class ReportFilterViewModel
    {
        public string SelectedFullName { get; set; }
        public List<Users> Users { get; set; } = new List<Users>();
        public bool IsSubmitted { get; set; }

    }
}

