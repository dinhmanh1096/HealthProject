namespace NewProject.Data
{
    public class User
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FistName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public int RoleID { get; set; }
        
        //
        public Role Role { get; set; }
        public ICollection<Workout> Workouts { get; set; }
    }
}
