using Domain;
using FluentValidation;

namespace Application.Activities
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(x => x.Title).NotEmpty(); // Title is required
            RuleFor(x => x.Description).NotEmpty(); // Description is required
            RuleFor(x => x.Date).NotEmpty(); // Date is required
            RuleFor(x => x.Category).NotEmpty(); // Category is required
            RuleFor(x => x.City).NotEmpty(); // City is required
            RuleFor(x => x.Venue).NotEmpty(); // Venue is required
        }
    }
}
