using System.ComponentModel.DataAnnotations;

namespace NewProject.Data
{
    public class Sport
    {
        
        public int SportID { get; set; }
        [Required]
        public string SportName { get; set;}
        public string? SportDescription { get; set;} = string.Empty;
        public ICollection<Workout> Workouts { get; set; }
    }
}
