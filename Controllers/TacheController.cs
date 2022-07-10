
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

    public class TacheController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TacheController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]

        public async Task<ActionResult<RS<Tache>>> Get(string order = "asc", int page = 1, int take = 10)
        {
            var res = order == "asc" ? await _context.Taches.OrderBy(a => a.DateTache).ToListAsync() :
                await _context.Taches.OrderByDescending(a => a.DateTache).ToListAsync()
                ;


            var result = new RS<Tache>();
            result.data = res.Skip((page - 1) * take).Take(take).ToList();
            result.total = res.Count;
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tache>> Get(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
                return BadRequest("Tache not found.");
            return Ok(tache);
        }
        [Authorize]

        [HttpPost]
        public async Task<ActionResult<Tache>> Add([FromForm] TachePostModel model)
        {
            string owner = User.FindFirst(ClaimTypes.Name)?.Value;
            string mail = User.FindFirst(ClaimTypes.Email)?.Value;
            var tache = _mapper.Map<Tache>(model);
            tache.Compte = mail;

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
        [Authorize]

        [HttpPut]
        public async Task<ActionResult<Tache>> Update([FromForm] TachePutModel request)
        {
           
            var record = _mapper.Map<Tache>(request);
            if (request.NewFile != null)
            {
                var newName = DateTime.Now.Ticks + Path.GetExtension(request.NewFile.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", newName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await request.NewFile.CopyToAsync(stream);
                    record.File = "Uploads/" + newName;
                }
            }
            _context.Taches.Update(record);

            await _context.SaveChangesAsync();

            return Ok(record);
        }
        [Authorize]

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
