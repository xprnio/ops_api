using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services;
using OPS_API.Resources;

namespace OPS_API.Controllers
{
    [ApiController]
    [Route("/api/operations")]
    public class OperationsController : Controller
    {
        private IOperationService _opService;
        private IMessageService _messageService;
        private IEquipmentService _equipmentService;
        private IMapper _mapper;

        private static string[] Names =
        {
            "Cherise Baxter",
            "Rosann Condra",
            "Geraldine Boll",
            "Cordell Woodrow",
            "Savanna Suman",
            "Verna Dolce",
            "Neva Stallcup",
            "Cecila Coloma",
            "Obdulia Birchard",
            "Dominga Wymer",
            "Cordelia Poarch",
            "Cristie Tune",
            "Jospeh Feng",
            "Brandie Yeh",
            "Jenine Caulfield",
            "Arlena Rubalcava",
            "Aide Avelar",
            "Wayne Hickey",
            "Dave Rackham",
            "Alesha Swett"
        };

        public OperationsController(
            IOperationService opService,
            IMessageService messageService,
            IEquipmentService equipmentService,
            IMapper mapper)
        {
            _opService = opService;
            _messageService = messageService;
            _equipmentService = equipmentService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OperationResource>> FindByIdAsync(Guid id)
        {
            var opResult = await _opService.FindByIdAsync(id);

            if (!opResult.Success)
                return NotFound(new {opResult.Message});

            var operation = opResult.Value;

            var rescuersResult = await _opService.LoadRescuersAsync(operation);

            if (!rescuersResult.Success)
                return BadRequest(new {rescuersResult.Message});

            var equipmentResult = await _opService.LoadEquipmentAsync(operation);

            if (!equipmentResult.Success)
                return BadRequest(new {equipmentResult.Message});

            var resource = _mapper.Map<Operation, OperationResource>(operation);

            resource.Rescuers = resource.Rescuers
                .OrderBy(n => DateTime.Parse(n.EstimatedTimeOfArrival))
                .ThenBy(n => DateTime.Parse(n.AvailableUntil))
                .ToList();

            return Ok(new {Operation = resource});
        }

        [HttpPost]
        public async Task<IActionResult> CreateOperationAsync([FromBody] MissingPersonDocument document)
        {
            var operation = _mapper.Map<Operation>(document);

            var equipmentResult = await _equipmentService.FindByIdsAsync(document.EquipmentRequests);

            if (!equipmentResult.Success)
                return BadRequest(new {equipmentResult.Message});

            if (operation.RequestedEquipment == null)
            {
                operation.RequestedEquipment = new List<EquipmentRequest>();
            }

            foreach (var equipment in equipmentResult.Value)
            {
                operation.RequestedEquipment.Add(new EquipmentRequest
                {
                    Equipment = equipment,
                    Operation = operation
                });
            }

            var opResult = await _opService.CreateAsync(operation);

            if (!opResult.Success)
                return BadRequest(new
                {
                    Message = $"Unable to create operation: {opResult.Message}"
                });
            var rand = new Random();

            for (var i = 20; i > 0; i--)
            {
                var name = Names[rand.Next(0, Names.Length - 1)];
                var rescuer = new Rescuer
                {
                    Name = name,
                    Email = $"{name.Split(" ").Join(".")}@example.com",
                    PhoneNumber = $"+666{rand.Next(58000000, 59999999)}",
                    Age = (ushort) rand.Next(18, 40),
                    EstimatedTimeOfArrival = DateTime.Now
                        .AddMinutes(rand.Next(0, 60)),
                    AvailableUntil = DateTime.Now
                        .AddMinutes(30)
                        .AddMinutes(rand.Next(1 * 4, 6 * 4) * 15),
                    Inventory = new List<EquipmentInventory>()
                };

                operation.RequestedEquipment
                    .ConvertAll(x => new EquipmentInventory
                    {
                        EquipmentRequest = x,
                        Rescuer = rescuer
                    })
                    .Where(x => rand.Next(0, 5) > 2)
                    .ToList()
                    .ForEach(n => rescuer.Inventory.Add(n));

                await _opService.JoinAsync(opResult.Value.Id, rescuer);
            }

            var resource = _mapper.Map<OperationResource>(opResult.Value);
            return Created(resource.Id, new {Operation = resource});
        }

        [HttpPost("{id:guid}/join")]
        public async Task<IActionResult> JoinOperationAsync(Guid id, [FromBody] JoinOperationResource join)
        {
            var rescuer = _mapper.Map<Rescuer>(join);

            var requestResult = await _equipmentService.FindRequestsByOperationIdAsync(id);

            if (!requestResult.Success)
                return BadRequest(new {requestResult.Message});

            if (rescuer.Inventory == null)
                rescuer.Inventory = new List<EquipmentInventory>();

            foreach (var request in requestResult.Value)
            {
                rescuer.Inventory.Add(new EquipmentInventory
                {
                    Rescuer = rescuer,
                    EquipmentRequest = request
                });
            }

            var result = await _opService.JoinAsync(id, rescuer);
            if (!result.Success)
                return BadRequest(new
                {
                    Message = $"Unable to join operation: {result.Message}"
                });

            var resource = _mapper.Map<RescuerResource>(result.Value);
            return Ok(new {Rescuer = resource});
        }

        [HttpPost("{id:guid}/broadcast")]
        public async Task<IActionResult> BroadcastAsync(Guid id, [FromBody] BroadcastResource broadcast)
        {
            var opResult = await _opService.FindByIdAsync(id);
            var operation = opResult.Value;

            if (!opResult.Success)
                return NotFound(new {opResult.Message});

            var rescueResult = await _opService.LoadRescuersAsync(operation);

            if (!rescueResult.Success)
                return BadRequest(new {rescueResult.Message});

            try
            {
                foreach (var rescuer in operation.Rescuers)
                {
                    await _messageService.SendMessage(rescuer.PhoneNumber, broadcast.Message);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new {e.Message});
            }
        }
    }
}