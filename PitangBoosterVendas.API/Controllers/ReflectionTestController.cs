using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;
using PitangBoosterVendas.Business.Imp.Business;
using PitangBoosterVendas.Entity.DTO;
using PitangBoosterVendas.Utils.Attributes;

namespace PitangBoosterVendas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReflectionTestController(IReflectionTestBusiness _reflectionTestBusiness) : ControllerBase
    {
        [HttpGet("executarTestesReflection")]
        public async Task ExecutarTestesReflection()
        {
            await _reflectionTestBusiness.ExecutarTestesReflection();
        }
    }
}
