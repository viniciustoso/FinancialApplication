using FinancialApplication.BI.Models;
using FinancialApplication.BI.Persistences.SqlServer;
using FinancialApplication.BI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApplication.BI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AverageGrossIncomeByStateController : Controller
    {
        readonly BIService BIService;

        public AverageGrossIncomeByStateController(IBIRepository biRepository)
        {
            BIService = new BIService(biRepository);
        }

        [HttpGet]
        public async Task<ActionResult<IList<AverageGrossIncomeByStateModel>>> Get()
        {
            try
            {
                return Ok(await BIService.GetAverageGrossIncomeByStateModel());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}