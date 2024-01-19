using Domain;
using MediatR;
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
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                await _context.SaveChangesAsync();               


            }

        }
    }
}
