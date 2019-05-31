using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPS_API.Domain.Services;
using OPS_API.Resources;

namespace OPS_API.Controllers
{
    [ApiController]
    [Route("/api/organizations")]
    public class OrganizationController : Controller
    {
        private IOrganizationService _orgService;
        private IMapper _mapper;

        public OrganizationController(IOrganizationService orgService, IMapper mapper)
        {
            _orgService = orgService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllAsync()
        {
            var organizations = await _orgService.ListAllAsync();
            var resources = _mapper.Map<IEnumerable<OrganizationResource>>(organizations);

            return Ok(new {Organizations = resources});
        }
    }
}