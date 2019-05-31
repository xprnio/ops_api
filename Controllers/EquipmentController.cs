using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPS_API.Domain.Models;
using OPS_API.Domain.Services;
using OPS_API.Resources;

namespace OPS_API.Controllers
{
    [ApiController]
    [Route("/api/equipment")]
    public class EquipmentController : Controller
    {
        private IEquipmentService _service;
        private IMapper _mapper;

        public EquipmentController(IEquipmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListEquipmentAsync()
        {
            var equipment = await _service.ListAllAsync();
            var resources = _mapper.Map<IEnumerable<EquipmentResource>>(equipment);

            return Ok(new {Equipment = resources});
        }
    }
}