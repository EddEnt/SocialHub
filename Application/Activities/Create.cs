using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        /*
        This class is used to create a new activity
        The Command class is used to send data to the server in order to create a new activity.
        It is a common practice in software development to use the term "command" to represent an
        action or request that needs to be executed.
        In this case, the Command class serves as a container for the data that will be sent to the server
        to create the activity.
        By implementing the IRequest interface, the Command class indicates that it is a request object
        that can be handled by the server.
        */
        public class Command : IRequest<Result<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                // The RuleFor method is used to specify the property that we want to validate.
                // In this case, we want to validate the Title property.
                // The NotEmpty method is used to specify that the Title property cannot be empty.
                // The WithMessage method is used to specify the error message that will be returned
                // if the validation fails.
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(
                    x => x.UserName == _userAccessor.GetUsername());

                var attendee = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = request.Activity,
                    IsHost = true
                };

                request.Activity.Attendees.Add(attendee);

                _context.Activities.Add(request.Activity);
                
                var result = await _context.SaveChangesAsync() > 0;
                
                if(!result)
                {
                    return Result<Unit>.Failure("Failed to create activity");
                }

                return Result<Unit>.Success(Unit.Value);


            }

        }
    }
}
