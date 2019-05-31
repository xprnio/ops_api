using System.Threading.Tasks;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IMessageService
    {
        Task<Response> SendMessage(string phoneNumber, string message);
    }
}