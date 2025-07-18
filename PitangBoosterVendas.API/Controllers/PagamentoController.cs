using Microsoft.AspNetCore.Mvc;
using PitangBoosterVendas.Business.IBusiness;

namespace PitangBoosterVendas.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController(IPagamentoBusiness pagamentoBusiness): ControllerBase
    {
       
    }
}
