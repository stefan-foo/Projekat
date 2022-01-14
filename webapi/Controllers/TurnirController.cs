using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models;
using Newtonsoft.Json.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TurnirController : ControllerBase
    {
        public EvidencijaContext Context { get; set; }
        public TurnirController(EvidencijaContext context) 
        {
            Context = context;
        }  

        [Route("PreuzmiTurnire")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiTurnire() 
        {
            try 
            { 
                return Ok(await Context.Turniri
                    .Select(p => new {
                        ID = p.TurnirID, 
                        Naziv = p.Naziv,
                        Drzava = p.Drzava.Naziv,
                        p.DatumOd, 
                        p.DatumDo,
                        p.BrojRundi,
                        p.TimeControl
                    }).ToListAsync());
            }
            catch(Exception e) { return BadRequest(e.Message); }
        }

        [Route("PreuzmiUcesnike/{id}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiUcesnike(int id)
        {
            try 
            {
                var turnir = await Context.Turniri.Where(t => t.TurnirID == id).FirstOrDefaultAsync();

                if (turnir == null) return BadRequest("Doslo je do greske");
                var ucesnici = await Context.Ucesnici
                    .Include(u => u.Turnir)
                    .Include(u => u.Igrac)
                    .ThenInclude(i => i.Titula)
                    .Where(u => u.Turnir.TurnirID == turnir.TurnirID).ToListAsync();
                return Ok (
                    ucesnici.Select(u => new {
                        Id = u.Igrac.IgracID,
                        Titula = u.Igrac.Titula.Title,
                        Ime = u.Igrac.Ime,
                        Prezime = u.Igrac.Prezime,
                        Drzava = u.Igrac.Drzava,
                        Mesto = u.Mesto,
                        Bodovi = u.Bodovi
                    }).ToList()
                );
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiPartije/{id}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPartije(int id)
        {
            try 
            {
                var turnir = await Context.Turniri.Where(t => t.TurnirID == id).FirstOrDefaultAsync();

                if (turnir == null) return BadRequest("Doslo je do greske");
                var partije = await Context.Partije
                    .Include(p => p.Turnir)
                    .Include(p => p.Beli)
                    .Include(p => p.Crni)
                    .Where(p => p.Turnir.TurnirID == turnir.TurnirID).ToListAsync();
                return Ok (
                    partije.Select(p => new {
                        BeliIme = p.Beli.Ime,
                        BeliPrezime = p.Beli.Prezime,
                        CrniIme = p.Crni.Ime,
                        CrniPrezime = p.Crni.Prezime,
                        Ishod = p.Ishod,
                        Runda = p.Runda,
                        BrPoteza = p.BrojPoteza,
                        Notacija = p.Notacija
                    }).ToList()
                );
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajTurnir")]
        [HttpPost]
        public async Task<ActionResult> DodajTurnir([FromBody]Turnir turnir)
        {
            try 
            {
                Context.Turniri.Add(turnir);
                await Context.SaveChangesAsync();
                return Ok(new {id = turnir.TurnirID});
            }
            catch (Exception e){
                return BadRequest(new { Message = e.Message });
            }
        }

        [Route("ObrisiTurnir/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiTurnir(int id)
        {
            try 
            {
                var turnir = await Context.Turniri.FindAsync(id);
                if (turnir == null) return BadRequest("Doslo je do greske");
                Context.Turniri.Remove(turnir);
                await Context.SaveChangesAsync();
                return Ok("Turnir uklonjen");
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniTurnir")]
        [HttpPut]
        public async Task<ActionResult> IzmeniTurnir([FromBody] Turnir turnir){
            var toupdate = await Context.Turniri.FindAsync(turnir.TurnirID);
            if (toupdate == null) return BadRequest("Turnir ne postoji");
            Context.Entry(toupdate).State = EntityState.Detached;
            try 
            {
                Context.Turniri.Update(turnir);
                await Context.SaveChangesAsync();
                return Ok($"Turnir je azuiran");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajPartiju")]
        [HttpPost]
        public async Task<ActionResult> DodajPartiju([FromBody] Partija partija){
            if (partija.BeliIgracID == partija.CrniIgracID)
                return BadRequest("Beli i crni igrac ne mogu imati istu vrednost");
            if (partija.BrojPoteza <= 0)
                return BadRequest("Broj poteza mora biti veci od 0");
            if (String.IsNullOrEmpty(partija.Notacija))
                return BadRequest("Notacija ne moze biti prazna");

            var beli = await Context.Igraci.FindAsync(partija.BeliIgracID);
            var crni = await Context.Igraci.FindAsync(partija.CrniIgracID);
            if (beli == null || crni == null) 
                return BadRequest("Igrac ne postoji");
            var turnir = await Context.Turniri.FindAsync(partija.TurnirID);
            if (turnir == null)
                return BadRequest("Zadati turnir ne postoji");
            try {
                Context.Partije.Add(partija);
                await Context.SaveChangesAsync();
                return Ok("Partija je dodata");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("DodajUcesnika/{turnirID}/{igracID}")]
        [HttpPost]
        public async Task<ActionResult> DodajUcesnika(int turnirID, int igracID){
            var igrac = await Context.Igraci.FindAsync(igracID);
            if (igrac == null) return BadRequest("Igrac ne postoji");
            var turnir = await Context.Turniri.FindAsync(turnirID);
            if (turnir == null) return BadRequest("Turnir ne postoji");
            var ucesnik = await Context.Ucesnici
                    .Where(uc => uc.IgracID == igracID 
                            && uc.TurnirID == turnirID).FirstOrDefaultAsync();
            if (ucesnik != null) return BadRequest("Dati igrac vec postoji na listi ucesnika turnira");
            Ucesnik novi = new Ucesnik {
                IgracID = igrac.IgracID,
                TurnirID = turnir.TurnirID,
                Bodovi = 0,
                Mesto = 0
            };
            try {
                Context.Ucesnici.Add(novi);
                await Context.SaveChangesAsync();
                return Ok("Ucesnik dodat");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }

        }
    }
}
