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
        }
    }
}
