using System.ComponentModel.DataAnnotations;

namespace NewProject.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        
    }
    public class RequestUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FistName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }

    }
}
