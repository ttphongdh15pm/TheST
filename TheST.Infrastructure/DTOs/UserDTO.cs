using TheST.Infrastructure.Enums;

namespace TheST.Infrastructure.DTOs
{
    public sealed class UserDTO
    {
        public string? DisplayName { get; set; }
        public string? ImagePath { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Invisible;
    }
}
