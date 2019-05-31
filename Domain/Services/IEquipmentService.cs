using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> ListAllAsync();
        Task<ValueResponse<IEnumerable<Equipment>>> FindByIdsAsync(IEnumerable<Guid> ids);
        Task<ValueResponse<IEnumerable<EquipmentRequest>>> FindRequestsByOperationIdAsync(Guid id);
    }
}