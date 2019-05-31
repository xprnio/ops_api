using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Domain.Services
{
    public interface IRescuerService
    {
        Task<IEnumerable<string>> ListUniquePhoneNumbers();
        Task<IEnumerable<Rescuer>> ListAllAsync();
        Task<ValueResponse<Rescuer>> FindByIdAsync(Guid id);
        Task<ValueResponse<Rescuer>> CreateAsync(Rescuer rescuer);
    }
}