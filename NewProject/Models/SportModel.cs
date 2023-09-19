namespace NewProject.Models
{
    public class SportModel
    {
        public int SportID { get; set; }
        public string SportName { get; set; }
        public string SportDescription { get; set; } = string.Empty;
    }
    public class RequestSportModel
    {
        public string SportName { get; set; }
        public string SportDescription { get; set; } = string.Empty;
    }
}
