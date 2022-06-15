
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Security.Claims;
using tracerapi.DTOs;
using tracerapi.Models;

namespace tracerapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    [ApiController]

    public class IncidentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };
        static readonly string[] scopeRequiredByApi2 = new string[] { "validations.access" };

        public IncidentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RS<Incident>>> Get(string statut= "", string priority ="", int page=1, int take=10)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _context.Incidents.Where(i => (string.IsNullOrEmpty(priority) || i.Priorite == priority) && 
            (string.IsNullOrEmpty(statut) || i.Statut == statut) && (string.IsNullOrEmpty(owner) || i.Owner == owner)
            ).ToListAsync();
              
            var result = new RS<Incident>();
            result.data= res.Skip((page-1)*take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }
        [HttpGet("getValidations")]
        public async Task<ActionResult<RS<Incident>>> getValidations(string statut = "", string priority = "", int page = 1, int take = 10)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi2);
            var res = await _context.Incidents.Where(i =>i.AskToClose== CloseStatus.askedToBeClosed && (string.IsNullOrEmpty(priority) || i.Priorite == priority) &&
            (string.IsNullOrEmpty(statut) || i.Statut == statut) 
            ).ToListAsync();

            var result = new RS<Incident>();
            result.data = res.Skip((page - 1) * take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> Get(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
                return BadRequest("incident not found.");
            return Ok(incident);
        }

        [HttpPost]
        public async Task<ActionResult<Incident>> AddIncident([FromForm]IncidentPostModel model)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var incident = _mapper.Map<Incident>(model);
          if(model.NewFile!=null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(model.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await model.NewFile.CopyToAsync(stream);
                    incident.File = "Uploads/" + newName;
                }
            }
            incident.Owner = owner;
            _context.Incidents.Add(incident);
            
            await _context.SaveChangesAsync();

            return Ok(incident);
        }

        [HttpPut]
        public async Task<ActionResult<Incident>> UpdateIncident([FromForm] IncidentPutModel request)
        {
            var dbincident = await _context.Incidents.FindAsync(request.Id);
            if (dbincident == null)
                return BadRequest("incident not found.");
            var incident = _mapper.Map<Incident>(request);
            dbincident = incident;
            if (request.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(request.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await request.NewFile.CopyToAsync(stream);
                    dbincident.File = "Uploads/" + newName;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(dbincident);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Incident>>> Delete(int id)
        {
            var dbincident = await _context.Incidents.FindAsync(id);
            if (dbincident == null)
                return BadRequest("incident not found.");

            _context.Incidents.Remove(dbincident);
            await _context.SaveChangesAsync();

            return Ok("deleted !!");
        }

    }
}
