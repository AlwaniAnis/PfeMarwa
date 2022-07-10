using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Security.Claims;
using tracerapi.DTOs;

namespace tracerapi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class StatisticsController : ControllerBase
    {
        private readonly DataContext _context;


        public StatisticsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<StatsDto>> Get(string owner="")
        {
            var res = new StatsDto();
            res.interventionsCount = _context.Interventions.Where(i =>  string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            res.TachesCount=_context.Taches.Where(i => string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            res.IncidentsCount = _context.Incidents.Where(i => string.IsNullOrEmpty(owner) || i.Owner == owner).Count();
            return Ok(res);
        }
        

    }
}
