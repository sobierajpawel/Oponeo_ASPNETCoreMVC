using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oponeo.CustomerManagement.ControllerBasedWebAPI.Infrastructure;

namespace Oponeo.CustomerManagement.ControllerBasedWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TokenGeneratorService _tokenGeneratorService;
        public AccountController(TokenGeneratorService tokenGeneratorService)
        {
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(string userEmail)
        {
            if (userEmail == "test@o2.pl")
            {
                return Ok(_tokenGeneratorService.GetToken(userEmail));
            }

            return BadRequest();
        }
    }
}
