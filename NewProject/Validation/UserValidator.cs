using FluentValidation;
using NewProject.Models;

namespace NewProject.Validation
{
    public class UserValidator : AbstractValidator<RequestUserModel>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
            RuleFor(u => u.FistName).MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
            RuleFor(u => u.LastName).MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
            RuleFor(u => u.Address).MaximumLength(250).Matches(@"^[A-Za-z\s]*$");
            RuleFor(u => u.PhoneNumber).MaximumLength(10).Matches(@"^[0-9]*$");
            RuleFor(u => u.Email).EmailAddress().NotEmpty();
            RuleFor(u => u.RoleID).NotEmpty();
        }
    }
}
