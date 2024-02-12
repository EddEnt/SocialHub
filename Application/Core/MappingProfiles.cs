using Application.Activities;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Mapping profile for the Activity class
            CreateMap<Activity, Activity>();

            CreateMap<Activity, ActivityDto>()
                .ForMember(
                destinationMember => destinationMember.HostUsername, 
                options => options.MapFrom(
                    sourceMember => sourceMember.Attendees.FirstOrDefault(x => x.IsHost)
                    .AppUser.UserName));

            CreateMap<ActivityAttendee, Profiles.Profile>()
                .ForMember(destinationMember => destinationMember.DisplayName,
                options => options.MapFrom(sourceMember => sourceMember.AppUser.DisplayName))

                .ForMember(destinationMember => destinationMember.Username,
                options => options.MapFrom(sourceMember => sourceMember.AppUser.UserName))

                .ForMember(destinationMember => destinationMember.Bio,
                options => options.MapFrom(sourceMember => sourceMember.AppUser.Biography));
        }
    }
}
