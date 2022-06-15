
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

    public class InterventionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public InterventionController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RS<Intervention>>> Get(string type = "", int page = 1, int take = 10)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _context.Interventions.Where(i => (string.IsNullOrEmpty(type) || i.Type == type) && (string.IsNullOrEmpty(owner) || i.Owner == owner)
        ).ToListAsync();

            var result = new RS<Intervention>();
            result.data = res.Skip((page - 1) * take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> Get(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
                return BadRequest("incident not found.");
            return Ok(intervention);
        }

        [HttpPost]
        public async Task<ActionResult<Intervention>> Add([FromForm] InterventionPostModel model)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var intervention = _mapper.Map<Intervention>(model);
            if (model.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(model.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await model.NewFile.CopyToAsync(stream);
                    intervention.File = "Uploads/" + newName;
                }
            }
            intervention.Owner = owner;
            _context.Interventions.Add(intervention);

            await _context.SaveChangesAsync();

            return Ok(intervention);
        }

        [HttpPut]
        public async Task<ActionResult<Intervention>> Update([FromForm] InterventionPutModel request)
        {
            var dbIntervention = await _context.Interventions.FindAsync(request.Id);
            if (dbIntervention == null)
                return BadRequest("Intervention not found.");
            var intervention = _mapper.Map<Intervention>(request);
            dbIntervention = intervention;
            if (request.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(request.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await request.NewFile.CopyToAsync(stream);
                    dbIntervention.File = "Uploads/" + newName;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(dbIntervention);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Intervention>>> Delete(int id)
        {
            var dbincident = await _context.Interventions.FindAsync(id);
            if (dbincident == null)
                return BadRequest("incident not found.");

            _context.Interventions.Remove(dbincident);
            await _context.SaveChangesAsync();

            return Ok(await _context.Interventions.ToListAsync());
        }

    }
}
