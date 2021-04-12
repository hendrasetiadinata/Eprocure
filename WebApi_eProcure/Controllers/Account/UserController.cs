using ApplicationCore.Base;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_eProcure.Controllers.Account
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class UserController : WebApiControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _user.GetUser());
        }

        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            return Ok(await _user.GetUser(id));
        }
    }
}
