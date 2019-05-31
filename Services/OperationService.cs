using System;
using System.Threading.Tasks;
using HandlebarsDotNet;
using OPS_API.Domain.Models;
using OPS_API.Domain.Repositories;
using OPS_API.Domain.Services;
using OPS_API.Domain.Services.Communication;

namespace OPS_API.Services
{
    public class OperationService : IOperationService
    {
        private IOperationRepository _opRepository;
        private IUserRepository _userRepository;
        private IRescuerService _rescuerService;
        private IMessageService _messageService;
        private IUnitOfWork _work;

        public OperationService(
            IOperationRepository opRepository,
            IUserRepository userRepository,
            IRescuerService rescuerService,
            IMessageService messageService,
            IUnitOfWork work)
        {
            _opRepository = opRepository;
            _userRepository = userRepository;
            _rescuerService = rescuerService;
            _messageService = messageService;
            _work = work;
        }

        public async Task<ValueResponse<Rescuer>> JoinAsync(Guid id, Rescuer rescuer)
        {
            var opResult = await FindByIdAsync(id);

            if (!opResult.Success)
                return new ValueResponse<Rescuer>(opResult.Message);

            try
            {
                rescuer.Operation = opResult.Value;

                var result = await _rescuerService.CreateAsync(rescuer);
                await _messageService.SendMessage(
                    rescuer.PhoneNumber,
                    "Kogunemine toimub kell 17:30 aadressil Telliskivi 60a, 10412 Tallinn. https://goo.gl/maps/rS1Pm5yPvG6z9NYa7"
                );

                return !result.Success
                    ? new ValueResponse<Rescuer>($"Error joining operation {id}: {result.Message}")
                    : new ValueResponse<Rescuer>(result.Value);
            }
            catch (Exception e)
            {
                return new ValueResponse<Rescuer>(e.Message);
            }
        }

        public async Task<ValueResponse<Operation>> FindByIdAsync(Guid id)
        {
            var op = await _opRepository.FindByIdAsync(id);

            return op == null
                ? new ValueResponse<Operation>("Operation not found")
                : new ValueResponse<Operation>(op);
        }

        public async Task<ValueResponse<Operation>> CreateAsync(Operation operation)
        {
            try
            {
                await _opRepository.AddAsync(operation);
                await _work.CompleteTask();

                var numbers = await _userRepository.ListAllPhoneNumbers();
                var message =
                    $"Palun aita leida kadunud {operation.MissingPerson.Name}. Viimati nähtud: {operation.MissingPerson.LastSeenInformation}. http://ragnarlaud.me/details/{operation.Id.ToString()}";

                foreach (var number in numbers)
                {
                    await _messageService.SendMessage(number, message);
                }

                return new ValueResponse<Operation>(operation);
            }
            catch (Exception e)
            {
                return new ValueResponse<Operation>(e.Message);
            }
        }

        public async Task<ValueResponse<Operation>> LoadRescuersAsync(Operation operation)
        {
            try
            {
                await _opRepository.LoadRescuersAsync(operation);
                return new ValueResponse<Operation>(operation);
            }
            catch (Exception e)
            {
                return new ValueResponse<Operation>(e.Message);
            }
        }

        public async Task<ValueResponse<Operation>> LoadEquipmentAsync(Operation operation)
        {
            try
            {
                await _opRepository.LoadEquipmentAsync(operation);
                return new ValueResponse<Operation>(operation);
            }
            catch (Exception e)
            {
                return new ValueResponse<Operation>(e.Message);
            }
        }
    }
}