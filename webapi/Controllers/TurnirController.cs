using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
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

        [Route("PreuzmiTurnire/{naziv}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiTurnire(string naziv) 
        {
            var turniri = await Context.Turniri
                                .Include(tr => tr.Drzava)
                                .Where(tr => tr.Naziv!.Contains(naziv)).ToListAsync();
            try 
            { 
                if (turniri.Count == 0) return NotFound("Ne postoji trazeni turnir");
                return Ok
                (   turniri.Select(p => new {
                        ID = p.TurnirID, 
                        Naziv = p.Naziv,
                        DrzavaID = p.DrzavaID,
                        Drzava = p.Drzava.Naziv,
                        p.DatumOd, 
                        p.DatumDo,
                        p.BrojRundi,
                        p.TimeControl
                    })
                );
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
                if (turnir == null) return BadRequest("Turnir ne postoji");

                var ucesnici = await Context.Ucesnici
                    .Include(u => u.Turnir)
                    .Include(u => u.Igrac)
                    .ThenInclude(i => i.Titula)
                    .Include(u => u.Igrac)
                    .ThenInclude(i => i.Drzava)
                    .Where(u => u.Turnir.TurnirID == turnir.TurnirID).ToListAsync();
                return Ok (
                    ucesnici.Select(u => new {
                        Id = u.Igrac.IgracID,
                        Titula = u.Igrac.Titula.Title,
                        Ime = u.Igrac.Ime,
                        Prezime = u.Igrac.Prezime,
                        Drzava = u.Igrac.Drzava?.Naziv,
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
                if (turnir == null) return BadRequest("Turnir ne postoji");

                var partije = await Context.Partije
                    .Include(p => p.Turnir)
                    .Include(p => p.Beli)
                    .Include(p => p.Crni)
                    .Where(p => p.Turnir.TurnirID == turnir.TurnirID).ToListAsync();
                return Ok (
                    partije.Select(p => new {
                        ID = p.ID,
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
            if (turnir.DrzavaID != null)
            {
                Drzava drzava = await Context.Drzave.FindAsync(turnir.DrzavaID);
                if (drzava == null) return BadRequest("Drzava ne postoji");
            }
            if (String.IsNullOrEmpty(turnir.Naziv))
            {
                return BadRequest("Turnir mora imati naziv");
            }
            else 
            {
                TextInfo title = new CultureInfo("en-US", false).TextInfo;
                turnir.Naziv = title.ToTitleCase(turnir.Naziv);
            }

            try 
            {
                Context.Turniri.Add(turnir);
                await Context.SaveChangesAsync();
                return Ok(
                    new {
                        ID = turnir.TurnirID, 
                        Naziv = turnir.Naziv,
                        DrzavaID = turnir.DrzavaID,
                        Drzava = turnir.Drzava.Naziv,
                        turnir.DatumOd, 
                        turnir.DatumDo,
                        turnir.BrojRundi,
                        turnir.TimeControl
                    });
            }
            catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [Route("ObrisiTurnir/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiTurnir(int id)
        {
            try 
            {
                var turnir = await Context.Turniri.FindAsync(id);
                if (turnir == null) return BadRequest("Turnir ne postoji");

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
            if (string.IsNullOrEmpty(turnir.Naziv)){
                return BadRequest("Turnir mora imati naziv");
            }
            if (turnir.DrzavaID != null)
            {
                Drzava drzava = await Context.Drzave.FindAsync(turnir.DrzavaID);
                if (drzava == null) return BadRequest("Drzava ne postoji");
            }
            TextInfo title = new CultureInfo("en-US", false).TextInfo;
            turnir.Naziv = title.ToTitleCase(turnir.Naziv);

            Turnir toupdate = await Context.Turniri.FindAsync(turnir.TurnirID);
            if (toupdate == null) return BadRequest("Turnir ne postoji");

            try 
            {
                toupdate.Naziv = turnir.Naziv;
                toupdate.DatumDo = turnir.DatumDo;
                toupdate.DatumOd = turnir.DatumOd;
                if (turnir.DrzavaID != null)
                    toupdate.DrzavaID = turnir.DrzavaID;
                toupdate.TimeControl = turnir.TimeControl;
                toupdate.BrojRundi = turnir.BrojRundi;

                await Context.SaveChangesAsync();
                return Ok(
                    new {
                        ID = toupdate.TurnirID, 
                        Naziv = toupdate.Naziv,
                        DrzavaID = toupdate.DrzavaID,
                        Drzava = toupdate.Drzava?.Naziv,
                        toupdate.DatumOd, 
                        toupdate.DatumDo,
                        toupdate.BrojRundi,
                        toupdate.TimeControl
                    });
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

            // var beli = await Context.Igraci.FindAsync(partija.BeliIgracID);
            // var crni = await Context.Igraci.FindAsync(partija.CrniIgracID);
            // if (beli == null || crni == null) 
            //     return BadRequest("Igrac ne postoji");

            var turnir = await Context.Turniri.FindAsync(partija.TurnirID);
            if (turnir == null)
                return BadRequest("Zadati turnir ne postoji");

            if (turnir.BrojRundi < partija.Runda)
                return BadRequest("Neodgovarajuca vrednost runde");

            List<Ucesnik> ucesnici = await Context.Ucesnici 
                .Where(uc => uc.TurnirID == partija.TurnirID).ToListAsync();
            if (ucesnici.Count == 0) return BadRequest("Greska");

            var beliUcestvuje = ucesnici.Where(uc => uc.IgracID == partija.BeliIgracID).FirstOrDefault();
            if (beliUcestvuje == null) return BadRequest("Beli nije ucesnik turnira");
            var crniUcestvuje = ucesnici.Where(uc => uc.IgracID == partija.CrniIgracID).FirstOrDefault();
            if (crniUcestvuje == null) return BadRequest("Crni nije ucesnik turnira");

            if (partija.Ishod == "1-1")
            {
                crniUcestvuje.Bodovi += (float)0.5;
                beliUcestvuje.Bodovi += (float)0.5;
            }
            else if (partija.Ishod == "1-0")
            {
                beliUcestvuje.Bodovi += (float)1;
            }
            else 
            {
                crniUcestvuje.Bodovi += (float)1;
            }

            try {
                Context.Partije.Add(partija);
                Helper.SortirajUcesnike(ucesnici);

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

        [Route("UkloniPartiju/{PartijaId}")]
        [HttpDelete]
        public async Task<ActionResult> UkloniPartiju(int PartijaId){
            var partija = await Context.Partije.FindAsync(PartijaId);
            if (partija == null) return BadRequest("Partija ne postoji");
            
            List<Ucesnik> ucesnici = await Context.Ucesnici 
                .Where(uc => uc.TurnirID == partija.TurnirID).ToListAsync();
            if (ucesnici.Count == 0) return BadRequest("Greska");

            var beliUcestvuje = ucesnici.Where(uc => uc.IgracID == partija.BeliIgracID).FirstOrDefault();
            if (beliUcestvuje == null) return BadRequest("Beli nije ucesnik turnira");
            var crniUcestvuje = ucesnici.Where(uc => uc.IgracID == partija.CrniIgracID).FirstOrDefault();
            if (crniUcestvuje == null) return BadRequest("Crni nije ucesnik turnira");


            try {
                Context.Partije.Remove(partija);
                if (partija.Ishod == "1-1"){
                    beliUcestvuje.Bodovi -= (float)0.5;
                    crniUcestvuje.Bodovi -= (float)0.5;
                }
                else if (partija.Ishod == "1-0"){
                    beliUcestvuje.Bodovi -= (float)1;
                }
                else {
                    crniUcestvuje.Bodovi -= (float)1;
                }
                Helper.SortirajUcesnike(ucesnici);
                await Context.SaveChangesAsync();
                return Ok("Partija uklonjena");
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}
