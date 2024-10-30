
namespace NWBackendAPI.Models
{
    public class LoggedInUser
    {
        public string? Username { get; set; }
        public int? AccesslevelId { get; set; }
        public string? Token { get; set; }
    }
}
