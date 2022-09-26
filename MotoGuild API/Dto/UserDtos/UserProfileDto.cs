using MotoGuild_API.Dto.EventDtos;
using MotoGuild_API.Dto.GroupDtos;
using MotoGuild_API.Dto.RideDtos;
using MotoGuild_API.Dto.RouteDtos;

namespace MotoGuild_API.Dto.UserDtos
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? PhoneNumber { get; set; }
        public double Rating { get; set; }
        public ICollection<GroupUserProfilDto> Groups { get; set; }
        public ICollection<GroupUserProfilDto> OwnedGroups { get; set; }
        public ICollection<GroupUserProfilDto> PendingGroups { get; set; }
        public ICollection<EventUserProfileDto> Events { get; set; }
        public ICollection<EventUserProfileDto> OwnedEvents { get; set; }
        public ICollection<RideUserProfileDto> Rides { get; set; }
        public ICollection<RideUserProfileDto> OwnedRides { get; set; }
        public ICollection<RouteUserProfilDto> Routes { get; set; }
    }
}
