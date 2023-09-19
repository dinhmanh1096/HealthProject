using System.ComponentModel.DataAnnotations;

namespace NewProject.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public int RoleID { get; set; }
    }
}
