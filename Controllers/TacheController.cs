
using AutoMapper;
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

    public class TacheController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public TacheController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RS<Tache>>> Get(string type = "", int page = 1, int take = 10)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _context.Taches.Where(i => ( string.IsNullOrEmpty(type) || i.Type == type) && (string.IsNullOrEmpty(owner) || i.Owner == owner)
        ).ToListAsync();

            var result = new RS<Tache>();
            result.data = res.Skip((page - 1) * take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tache>> Get(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
                return BadRequest("Tache not found.");
            return Ok(tache);
        }

        [HttpPost]
        public async Task<ActionResult<Tache>> Add([FromForm] TachePostModel model)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            string owner = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var tache = _mapper.Map<Tache>(model);
            if (model.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(model.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await model.NewFile.CopyToAsync(stream);
                    tache.File = "Uploads/" + newName;
                }
            }
            tache.Owner = owner;
            _context.Taches.Add(tache);

            await _context.SaveChangesAsync();

            return Ok(tache);
        }

        [HttpPut]
        public async Task<ActionResult<Tache>> Update([FromForm] TachePutModel request)
        {
            var dbrecord = await _context.Taches.FindAsync(request.Id);
            if (dbrecord == null)
                return BadRequest("incident not found.");
            var record = _mapper.Map<Tache>(request);
            dbrecord = record;
            if (request.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(request.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await request.NewFile.CopyToAsync(stream);
                    dbrecord.File = "Uploads/" + newName;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(dbrecord);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Tache>>> Delete(int id)
        {
            var dbtache = await _context.Taches.FindAsync(id);
            if (dbtache == null)
                return BadRequest("incident not found.");

            _context.Taches.Remove(dbtache);
            await _context.SaveChangesAsync();

            return Ok(await _context.Taches.ToListAsync());
        }

    }
}
