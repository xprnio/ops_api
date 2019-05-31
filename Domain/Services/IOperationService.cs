using System;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IOperationService
    {
        Task<ValueResponse<Operation>> FindByIdAsync(Guid id);
        Task<ValueResponse<Rescuer>> JoinAsync(Guid id, Rescuer rescuer);
        Task<ValueResponse<Operation>> CreateAsync(Operation operation);
        Task<ValueResponse<Operation>> LoadRescuersAsync(Operation operation);
        Task<ValueResponse<Operation>> LoadEquipmentAsync(Operation operation);
    }
}