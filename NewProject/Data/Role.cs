using System.ComponentModel.DataAnnotations;

namespace NewProject.Data
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
