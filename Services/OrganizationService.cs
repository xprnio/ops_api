using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class OrganizationService : IOrganizationService
    {
        private IOrganizationRepository _orgRepository;

        public OrganizationService(IOrganizationRepository orgRepository)
        {
            _orgRepository = orgRepository;
        }

        public async Task<IEnumerable<Organization>> ListAllAsync()
        {
            return await _orgRepository.ListAllAsync();
        }

        public async Task<ValueResponse<Organization>> FindByIdAsync(Guid id)
        {
            var org = await _orgRepository.FindByIdAsync(id);

            return org == null
                ? new ValueResponse<Organization>("Organization not found")
                : new ValueResponse<Organization>(org);
        }

        public Task<ValueResponse<Organization>> CreateAsync(Organization organization)
        {
            throw new NotImplementedException();
        }
    }
}