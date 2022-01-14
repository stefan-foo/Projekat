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
    public class DrzavaController : ControllerBase
    {
        public EvidencijaContext Context { get; set; }
        public DrzavaController(EvidencijaContext context) {
            Context = context;
        }  

        [HttpGet]
        [Route("Preuzmi")]
        public async Task<ActionResult> PreuzmiDrzave(){
            return Ok(await Context.Drzave.Select(d => new {
                d.DrzavaID,
                d.Naziv
            }).ToListAsync());
        }
    }
}
