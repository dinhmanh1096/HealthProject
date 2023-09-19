using FluentValidation;
using NewProject.Models;

namespace NewProject.Validation
{
    public class SportValidator: AbstractValidator<RequestSportModel>
    {
        public SportValidator()
        {
            RuleFor(s => s.SportName).NotEmpty().MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
            RuleFor(s => s.SportDescription).MaximumLength(450).Matches(@"^[A-Za-z\s]*$");
        }
    }
}
