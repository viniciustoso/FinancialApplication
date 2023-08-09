using FinancialApplication.Authentication.Models;
using FinancialApplication.Authentication.Persistences.MongoDB;
using FinancialApplication.Authentication.Services;
using FinancialApplication.Utility.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApplication.Authentication.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    public class AuthenticateController : ControllerBase
    {
        readonly AuthenticateService AuthenticateService;

        public AuthenticateController(
            IUsersRepository usersRepository
        )
        {
            AuthenticateService = new AuthenticateService(usersRepository);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<JwtTokenModel>> Login(UserModel user)
        {
            try
            {
                return Ok(await AuthenticateService.Authenticate(user));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
