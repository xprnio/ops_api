using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IAuthService
    {
        Task<ValueResponse<Rescuer>> AuthenticateAsync(string email, string password);
        Task<ValueResponse<Rescuer>> AuthenticateAsync(string token);
    }
}