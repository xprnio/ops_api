using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class EquipmentService : IEquipmentService
    {
        private IEquipmentRepository _repository;

        public EquipmentService(IEquipmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ValueResponse<Equipment>> FindByIdAsync(Guid id)
        {
            var equipment = await _repository.FindByIdAsync(id);

            return equipment == null
                ? new ValueResponse<Equipment>("Equipment not found")
                : new ValueResponse<Equipment>(equipment);
        }

        public async Task<IEnumerable<Equipment>> ListAllAsync()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<ValueResponse<IEnumerable<Equipment>>> FindByIdsAsync(IEnumerable<Guid> ids)
        {
            var equipment = await _repository.FindAllAsync(e => ids.Contains(e.Id));

            return new ValueResponse<IEnumerable<Equipment>>(equipment);
        }

        public async Task<ValueResponse<IEnumerable<EquipmentRequest>>> FindRequestsByOperationIdAsync(Guid id)
        {
            var requests = await _repository
                .FindAllRequestsAsync(r => r.Operation.Id == id);
            
            return new ValueResponse<IEnumerable<EquipmentRequest>>(requests);
        }
    }
}