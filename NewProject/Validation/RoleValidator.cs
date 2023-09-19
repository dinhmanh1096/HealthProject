using FluentValidation;
using NewProject.Models;

namespace NewProject.Validation
{
    public class RoleValidator : AbstractValidator<RequestRoleModel>
    {
        public RoleValidator()
        {
            RuleFor(r => r.RoleName).NotEmpty().MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
        }
    }
}
