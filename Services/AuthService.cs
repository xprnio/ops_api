using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class AuthService : IAuthService
    {
        public Task<ValueResponse<Rescuer>> AuthenticateAsync(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<ValueResponse<Rescuer>> AuthenticateAsync(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}