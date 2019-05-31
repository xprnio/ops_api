using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class RescuerService : IRescuerService
    {
        private IRescuerRepository _rescuerRepository;
        private IUnitOfWork _work;

        public RescuerService(IRescuerRepository rescuerRepository, IUnitOfWork work)
        {
            _rescuerRepository = rescuerRepository;
            _work = work;
        }

        public async Task<IEnumerable<string>> ListUniquePhoneNumbers()
        {
            return await _rescuerRepository.ListDistinctPhoneNumbersAsync();
        }

        public async Task<IEnumerable<Rescuer>> ListAllAsync()
        {
            return await _rescuerRepository.ListAllAsync();
        }

        public async Task<ValueResponse<Rescuer>> FindByIdAsync(Guid id)
        {
            var user = await _rescuerRepository.FindByIdAsync(id);

            return user == null
                ? new ValueResponse<Rescuer>("User not found")
                : new ValueResponse<Rescuer>(user);
        }

        public async Task<ValueResponse<Rescuer>> CreateAsync(Rescuer rescuer)
        {
            try
            {
                await _rescuerRepository.AddAsync(rescuer);
                await _work.CompleteTask();

                return new ValueResponse<Rescuer>(rescuer);
            }
            catch (Exception e)
            {
                return new ValueResponse<Rescuer>(e.Message);
            }
        }
    }
}