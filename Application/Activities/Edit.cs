using MediatR;
using Domain;
using Persistence;
using AutoMapper;
using FluentValidation;
using Application.Core;

namespace Application.Activities
{
    public class Edit
    {
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
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                if(activity == null)
                {
                    return null;
                }

                _mapper.Map(request.Activity, activity);
                
                var result = await _context.SaveChangesAsync() > 0;

                if(!result)
                {
                    return Result<Unit>.Failure("Failed to update activity");
                }
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
