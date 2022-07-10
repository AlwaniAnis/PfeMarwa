using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using tracerapi.DTOs;
using tracerapi.Models;

namespace tracerapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]

    public class EventController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        //static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };
        //static readonly string[] scopeRequiredByApi2 = new string[] { "validations.access" };

        public EventController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            //string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _context.Events.Where(i =>
            (i.Start >=DateTime.Today && i.Start <=DateTime.Today.AddMonths(1)) ).ToListAsync();

            
            return Ok(res);
        }
       
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var _event = await _context.Events.FindAsync(id);
            if (_event == null)
                return BadRequest("incident not found.");
            return Ok(_event);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Event>> AddIncident([FromBody] Event model)
        {
          
       
          
            _context.Events.Add(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Event>> Update([FromBody] Event request)
        {
            
           
            _context.Events.Update(request);
            await _context.SaveChangesAsync();

            return Ok(request);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var dbevent = await _context.Events.FindAsync(id);
            if (dbevent == null)
                return BadRequest("event not found.");

            _context.Events.Remove(dbevent);
            await _context.SaveChangesAsync();

            return Ok("deleted !!");
        }

    }
}