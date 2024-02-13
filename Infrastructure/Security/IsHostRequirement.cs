using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class IsHostRequirement : IAuthorizationRequirement
    {
    }

    public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IsHostRequirementHandler(DataContext dataContext, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext authorizationHandlerContext, 
            IsHostRequirement hostRequirement)
        {
            var userId = authorizationHandlerContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                //If the user is not logged in, the requirement is not met
                return Task.CompletedTask;
            }

            var activityId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var attendee = _dataContext.ActivityAttendees
                .AsNoTracking()
                .SingleOrDefaultAsync(attendee => attendee.AppUserId == userId && attendee.ActivityId == activityId).Result;
            if (attendee == null)
            {
                return Task.CompletedTask;
            }
            if (attendee.IsHost)
            {
                authorizationHandlerContext.Succeed(hostRequirement);
            }
            return Task.CompletedTask;
        }
    }

}
