using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using WebApi_eProcure.ActionFilter.ActionFilterAttributes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_eProcure.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;

        public AuthenticateController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        // GET: api/<AuthenticateController>
        [HttpPost]
        [ServiceFilter(typeof(AuthenticateFilterAttribute), Order = 2)]
        public async Task<ActionResult> Post([FromBody] AuthenticateRequest value)
        {
            return Ok(await _authenticate.GetTokenAsync(value));
        }
    }
}
