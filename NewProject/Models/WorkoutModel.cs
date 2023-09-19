namespace NewProject.Models
{
    public class WorkoutModel
    {
        public int WorkoutID { get; set; }
        public string WorkoutName { get; set; }
        public string Distance { get; set; }
        public string Speed { get; set; }
        public string Time { get; set; }
        public int SportID { get; set; }
        public int UserID { get; set; }
    }
    public class RequestWorkoutModel
    {
        public string WorkoutName { get; set; }
        public string Distance { get; set; }
        public string Speed { get; set; }
        public string Time { get; set; }
        public int SportID { get; set; }
        public int UserID { get; set; }
    }
}
