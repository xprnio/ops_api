using System.Threading.Tasks;
using OPS_API.Domain.Models;

namespace OPS_API.Domain.Repositories
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task LoadRescuersAsync(Operation operation);
        Task LoadEquipmentAsync(Operation operation);
    }
}