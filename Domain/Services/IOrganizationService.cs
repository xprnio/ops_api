using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> ListAllAsync();
        Task<ValueResponse<Organization>> FindByIdAsync(Guid id);
        Task<ValueResponse<Organization>> CreateAsync(Organization organization);
    }
}