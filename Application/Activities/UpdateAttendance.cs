using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class UpdateAttendance
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities
                    .Include(activity => activity.Attendees).ThenInclude(user => user.AppUser)

                    //SingleOrDefaultAsync returns the first element of a sequence that satisfies
                    //a specified condition and makes sure that there is only one element in the sequence
                    //Returns an exception if there is no element or more than one element
                    //FirstOrDefaultAsync does the same, but returns null if there is no element
                    .SingleOrDefaultAsync(activity => activity.Id == request.Id);
                if (activity == null) return null;

                var user = await _context.Users.FirstOrDefaultAsync(
                    user => user.UserName == _userAccessor.GetUsername());
                if (user == null) return null;

                var hostUsername = activity.Attendees.FirstOrDefault(
                    attendee => attendee.IsHost)?.AppUser?.UserName;

                var attendance = activity.Attendees.FirstOrDefault(
                    attendee => attendee.AppUser.UserName == user.UserName);

                //If the user is the host, the activity is cancelled
                if (attendance != null && hostUsername == user.UserName)
                    activity.IsCancelled = !activity.IsCancelled;
                //If the user is not the host, the user is removed from the attendees                
                if (attendance != null && hostUsername != user.UserName)
                    activity.Attendees.Remove(attendance);
                //If the user is not the host and is not in the attendees, the user is added to the attendees
                if (attendance == null)
                {
                    attendance = new ActivityAttendee
                    {
                        AppUser = user,
                        Activity = activity,
                        IsHost = false
                    };
                    activity.Attendees.Add(attendance);
                }

                var result = await _context.SaveChangesAsync() > 0;
                return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("A problem occured when updating attendance");
            }
        }
    }
}
