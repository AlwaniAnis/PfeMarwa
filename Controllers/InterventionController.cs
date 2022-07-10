
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

    [Route("api/[controller]")]
 

    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class InterventionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public InterventionController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<RS<Intervention>>> Get(string order = "asc", int page = 1, int take = 10)
        {
            var res =order=="asc"? await _context.Interventions.OrderBy(a => a.DateIntervention).ToListAsync():
                await _context.Interventions.OrderByDescending(a => a.DateIntervention).ToListAsync()
                ;
        

            var result = new RS<Intervention>();
            result.data = res.Skip((page - 1) * take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> Get(int id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            if (intervention == null)
                return BadRequest("incident not found.");
            return Ok(intervention);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Intervention>> Add([FromForm] InterventionPostModel model)
        {
            string owner = User.FindFirst(ClaimTypes.GivenName)?.Value;
            string mail = User.FindFirst(ClaimTypes.Email)?.Value;
            var intervention = _mapper.Map<Intervention>(model);
            intervention.Compte = mail;
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
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Intervention>> Update([FromForm] InterventionPutModel request)
        {
          
            var intervention = _mapper.Map<Intervention>(request);
       
            if (request.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(request.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await request.NewFile.CopyToAsync(stream);
                    intervention.File = "Uploads/" + newName;
                }
            }
            _context.Interventions.Update(intervention);

            await _context.SaveChangesAsync();

            return Ok(intervention);
        }
        [Authorize]
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
