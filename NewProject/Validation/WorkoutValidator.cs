using FluentValidation;
using NewProject.Models;

namespace NewProject.Validation
{
    public class WorkoutValidator : AbstractValidator<RequestWorkoutModel>
    {
        public WorkoutValidator()
        {
            RuleFor(w => w.WorkoutName).NotEmpty().MaximumLength(50).Matches(@"^[A-Za-z\s]*$");
            RuleFor(w => w.Distance).MaximumLength(250);
            RuleFor(w => w.Speed).MaximumLength(250);
            RuleFor(w => w.Time).MaximumLength(250);
            RuleFor(w => w.SportID).NotEmpty();
            RuleFor(w => w.UserID).NotEmpty();
        }
    }
}
