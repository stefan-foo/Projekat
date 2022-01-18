using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IgracController : ControllerBase
    {
        public EvidencijaContext Context { get; set; }
        public IgracController(EvidencijaContext context) {
            Context = context;
        }  

        [HttpGet]
        [Route("Preuzmi")]
        public async Task<ActionResult> PreuzmiIgrace(){
            try {
                return Ok(await Context.Igraci.Select(p => new {
                    ID = p.IgracID,
                    Titula = p.Titula.Title,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    Drzava = p.Drzava.Naziv,
                    Classical = p.ClassicalRating,
                    Blitz = p.BlitzRating,
                    Rapid = p.RapidRating
                }).ToListAsync());
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}
