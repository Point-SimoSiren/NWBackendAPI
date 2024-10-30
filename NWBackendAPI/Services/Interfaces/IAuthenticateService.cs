using NWBackendAPI.Models;

namespace NWBackendAPI.Services.Interfaces
{
    public interface IAuthenticateService
    {
        public LoggedInUser Authenticate(string username, string password);
    }
}
