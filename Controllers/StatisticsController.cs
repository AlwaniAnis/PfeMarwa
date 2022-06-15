using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Security.Claims;
using tracerapi.DTOs;

namespace tracerapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly DataContext _context;
        static readonly string[] scopeRequiredByApi2 = new string[] { "validations.access" };


        public StatisticsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<StatsDto>> Get(string owner="")
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi2);
            var res = new StatsDto();
            res.interventionsCount = _context.Interventions.Where(i =>  string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            res.TachesCount=_context.Taches.Where(i => string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            res.IncidentsCount = _context.Incidents.Where(i => string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            return Ok(res);
        }
        

    }
}
