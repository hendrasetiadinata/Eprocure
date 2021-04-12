using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using WebApi_eProcure.ActionFilter.ActionFilterAttributes;
using ApplicationCore.Base;
using ApplicationCore.Models;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_eProcure.Controllers.Tender
{
    [Produces("application/json")]
    [Route("api/Tender")]
    [ApiController]
    public class ProjectTenderController : WebApiControllerBase
    {
        private readonly ITender _tenders;

        public ProjectTenderController(ITender tenders)
        {
            _tenders = tenders;
        }

        // GET: api/<ProjectTenderController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _tenders.GetTender());
        }

        // GET api/<ProjectTenderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            return Ok(await _tenders.GetTender(id));
        }

        // POST api/<ProjectTenderController>
        [HttpPost]
        [ServiceFilter(typeof(AddTenderFilterAttribute), Order = 2)]
        public async Task<ActionResult> AddNewTender([FromBody] ApplicationCore.Entities.Tender value)
        {
            var domain = new ApplicationCore.Entities.Tender()
            {
                Id = Guid.NewGuid().ToString(),
                ClosingDate = value.ClosingDate,
                ReleaseDate = value.ReleaseDate,
                CreatorId = DataContext.User.UserId,
                CreatedTime = DataContext.EntryTime,
                Details = value.Details,
                Name = value.Name,
                RefNumber = value.RefNumber
            };

            await _tenders.AddTender(domain);

            return Ok();
        }

        // PUT api/<ProjectTenderController>/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(UpdateTenderFilterAttribute), Order = 2)]
        public async Task<ActionResult> UpdateTender([FromBody] ApplicationCore.Entities.Tender value)
        {
            var domain = new ApplicationCore.Entities.Tender()
            {
                Id = value.Id,
                ClosingDate = value.ClosingDate,
                ReleaseDate = value.ReleaseDate,
                LastUpdatedBy = DataContext.User.UserId,
                LastUpdatedTime = DataContext.EntryTime,
                Details = value.Details,
                Name = value.Name,
                RefNumber = value.RefNumber
            };

            await _tenders.UpdateTender(domain);

            return Ok();
        }

        // DELETE api/<ProjectTenderController>/5
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(DeleteTenderFilterAttribute), Order = 2)]
        public async Task<ActionResult> DeleteTender(string id)
        {
            await _tenders.DeleteTender(id);

            return Ok();
        }
    }
}
