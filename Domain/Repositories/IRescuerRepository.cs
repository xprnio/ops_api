using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;

namespace OPS_API.Domain.Repositories
{
    public interface IRescuerRepository : IGenericRepository<Rescuer>
    {
        Task<IEnumerable<string>> ListDistinctPhoneNumbersAsync();
    }
}