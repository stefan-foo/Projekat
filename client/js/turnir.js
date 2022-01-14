import { buttonClicked, createElement, crtajFormu, dateDisplayFormat } from "./helpers.js";
import { createElInner } from "./helpers.js";
import { Partija } from "./partija.js";
import { StranicaSahisti } from "./stranicaSahisti.js";
import { StranicaTurniri } from "./stranicaTurniri.js";
import { Ucesnik } from "./ucesnik.js";

export class Turnir {
    constructor(id, naziv, drzava, datumOd, datumDo, brRundi, timeControl){
        this.id=id;
        this.naziv=naziv;
        this.drzava=drzava;
        this.datumOd=new Date(datumOd);
        this.datumDo=new Date(datumDo);
        this.brRundi=brRundi;
        this.timeControl=timeControl;
        this.ucesnici = [];
        this.container=null;
        this.ucesniciPreuzeti=false;
        this.partijePreuzete=false;
    }

    async draw(parent) {
        this.container = createElement("div", parent, ["contTurnir"]);
        this.drawHeader(this.container);
        this.drawDetailsBody(this.container);
        this.drawPartBody(this.container);
        this.drawGamesBody(this.container);
    }

    drawDetailEl(parent, lblval, val){
        let row = createElement("div", parent);
        if (!val) val = "/";
        createElInner("label", `${lblval}${val}`, row);
    }

    drawHeader(parent) {
        const header = createElement("div", parent, ["contHeader"]);
        const status = createElement("div", header, ["status", "finished"]);
        createElement("i", status, ["fas", "fa-check-circle"]);
        createElement("div", status, ["fas", "loading"]);
        //modifikacija
        const izmeni = createElement("div", header, ["controls", "left"]);
        const izmeniBtn = createElInner("button", "Izmeni", izmeni, ["fas", "fa-edit", "izmeni", "headerBtn"]);
        const contForma = createElement("div", parent, ["contIzmeniForma"]);
        contForma.style.display = "none";
        this.drawModifPanel(contForma);
        izmeniBtn.onclick = (ev) => this.showModifPanel(contForma, ev);
        //brisanje turnira
        const obrisiBtn = createElement("button", izmeni, ["fas", "fa-trash-alt", "headerBtn"]);
        obrisiBtn.onclick = (ev) => this.obrisiTurnir();
        //prikaz osnovnih informacija
        const list = createElement("ul", header, ["info"]);
        createElInner("li", this.naziv, list);
        createElInner("li", this.datumOd.toLocaleDateString('en-GB'), list);
        createElInner("li", this.datumDo.toLocaleDateString('en-GB'), list);
        const controls = createElement("div", header, ["controls", "right"]);
        //prikaz partija i igraca
        this.drawControls(controls);
    }

    drawDetailsBody(parent){
        const details = createElement("div", parent, ["contDetails"]);
        details.style.display="none";
        this.drawDetailEl(details, "Pocinje: ", this.datumOd.toLocaleDateString('en-GB'));
        this.drawDetailEl(details, "Traje do: ", this.datumDo.toLocaleDateString('en-GB'));
        this.drawDetailEl(details, "Odrzava se u: ", this.drzava);
        this.drawDetailEl(details, "Broj rundi: ", this.brRundi);
        this.drawDetailEl(details, "Vremenska kontrola: ", this.timeControl);
        const header = parent.querySelector(".contHeader");
        header.onclick = (ev) => this.swapView(ev);
    }

    drawPartBody(parent){
        const cont = createElement("div", parent, ["contParticipants"]);
        this.drawPartControls(cont);
        cont.style.display="none";
        const cols = ["Titula", "Ime", "Prezime", "Drzava", "Bodovi", "Mesto"];
        const table = createElement("table", cont, ["partTable"]);
        const thead = createElement("thead", table);
        cols.forEach(el => createElInner("th", el, thead));
        createElement("tbody", table, ["playersBody"])
    }

    drawGamesBody(parent){
        const gamesCont = createElement("div", parent, ["contGames"]);
        gamesCont.style.display="none";
        const contControls = createElement("div", gamesCont, ["gamesControls"]);
        const contGeneral = createElement("div", gamesCont, ["contGeneral"]);
        const dodajBtn = createElInner("button", "Dodaj partiju", contControls, ["headerBtn"]);
        this.dodajPartijuForma(contGeneral);
        dodajBtn.onclick = (ev) => { 
            this.prikaziDodajFormu(ev, contGeneral);
        }
        const gamesBody = createElement("div", contGeneral, ["contGamesBody"]);
        const contInfo = createElement("div", gamesBody, ["contGamesInfo"]);
        createElement("div", gamesBody, ["contGamesDisplay"]).style.display="none";
        const table = createElement("table", contInfo, ["gamesTable"]);
        const thead = createElement("thead", table);
        const cols = ["Partija", "Ishod", "Runda", "Br. poteza"]
        cols.forEach(el => createElInner("th", el, thead));
        createElement("tbody", table, ["gamesBody"]);
    }

    drawControls(parent){
        const types = ["Ucesnici", "Partije"];
        let btn = null;
        types.forEach((el, index) => {
            btn = createElement("button", parent, ["headerBtn"]);
            btn.innerHTML = el;
            parent.appendChild(btn);
            if (index == 0) btn.onclick=(ev)=>this.prikaziUcesnike(ev);
            else btn.onclick=(ev)=>this.prikaziPartije(ev);
        });
    }
    
    prikaziDodajFormu(ev, cont){
        buttonClicked(ev);
        if (!this.ucesniciPreuzeti) this.preuzmiUcesnike();
        const forma = cont.querySelector(".add-game-cont");
        if (forma.style.display !== "none"){
            forma.style.display = "none";
        }
        else {
            forma.style.display = "block";
        }
    }

    async dodajPartijuForma(parent){
        const formCont = createElement("div", parent, ["add-game-cont", "form"]);
        formCont.style.display = "none";
        const labels = ["Beli", "Crni", "Ishod", "Runda", "Br. poteza", "Notacija"];
        const inputTypes = ["select", "select", "select", "input", "input", "textarea"];
        const ishodi = ["1-0", "1-1", "0-1"];
        let elements = [];
        let row, option;
        labels.forEach((lab, index) => {
            row = createElement("div", formCont, ["form-control"]);
            createElInner("label", `${lab}: `, row);
            elements.push(createElement(inputTypes[index], row));
        });
        ishodi.forEach(ish => {
            option = createElInner("option", ish, elements[2]);
            option.value = ish;
        });

        elements[0].classList.add("beliSelect");
        elements[1].classList.add("crniSelect");
        // await this.preuzmiUcesnike();

        // this.ucesnici.forEach(uc => {
        //     option = createElInner("option", uc.ime + " " + uc.prezime, elements[0]);
        //     option.value = uc.id;
        //     option = createElInner("option", uc.ime + " " + uc.prezime, elements[1]);
        //     option.value = uc.id;
        // })

        row = createElement("div", formCont, ["form-control"]);
        createElInner("button", "Dodaj", row, ["headerBtn"])
            .onclick = (ev) => this.dodajPartiju({
                beliId: elements[0].value,
                crniId: elements[1].value,
                ishod: elements[2].value,
                runda: elements[3].value,
                brPoteza: elements[4].value,
                notacija: elements[5].value,
                turnir: this
            });
    }

    drawPartControls(parent){
        const cont = createElement("div", parent, ["partControls"]);
        const select = createElement("select", cont, ["select-player"]);
        createElInner("button", "Dodaj", cont, ["headerBtn"]).onclick = (ev) => {
            this.dodajUcesnika(select.value);
        };
    }

    drawModifPanel(contForma) {
        crtajFormu(contForma);
        const sacuvajBtn = contForma.querySelector("button");
        sacuvajBtn.innerHTML = "Sačuvaj izmene";
    }

    showModifPanel(parent, ev){
        buttonClicked(ev);
        if (parent.style.display !== "none") {
            parent.style.display = "none";
            this.container.querySelector(".status").classList.replace("loading", "finished");
            return;
        }
        
        parent.style.display = "flex";
        this.container.querySelector(".status").classList.replace("finished", "loading");

        const naziv = parent.querySelector(".naziv");
        const pocetak = parent.querySelector(".pocetak");
        const kraj = parent.querySelector(".kraj");
        const drzava = parent.querySelector("select");
        const vrem = parent.querySelector(".vremenska-kontrola");
        const brRundi = parent.querySelector(".broj-rundi");

        naziv.value = this.naziv;
        kraj.value = dateDisplayFormat(this.datumDo);
        pocetak.value = dateDisplayFormat(this.datumOd);
        brRundi.value = this.brRundi;
        vrem.value = this.timeControl;

        parent.querySelector("button").onclick = (ev) => this.izmeniTurnir({ 
            naziv, pocetak, kraj, drzava, vrem, brRundi
        });
    }

    izmeniTurnir(turnir){
        let valresult = Turnir.validateTurnir(
            turnir.naziv.value, 
            turnir.brRundi.value,
            turnir.vrem.value, 
            turnir.pocetak.value, 
            turnir.kraj.value, 
        );
        if (!valresult.res){
            alert(valresult.msg);
            return;
        }
        {
            fetch("https://localhost:5001/Turnir/IzmeniTurnir", {
                method: 'PUT',
                headers: {
                    "Content-Type":"application/json"
                },
                body: JSON.stringify( {
                    turnirID : this.id,
                    naziv : turnir.naziv.value, 
                    brojRundi : turnir.brRundi.value,
                    timeControl : turnir.vrem.value, 
                    datumOd : turnir.pocetak.value, 
                    datumDo : turnir.kraj.value, 
                    drzavaID : turnir.drzava.value
                })
            }).then(res => {
                if (res.ok){
                    location.reload();
                }
                else 
                {
                    this.container.querySelector(".status").classList.replace("finished", "loading");
                    alert("Doslo je do greske");
                }
            });
        }
    }

    static dodajTurnir(naziv, pocetak, kraj, drzava, vrem, brRundi){
        return new Promise((resolve, reject) => {
            let validation = this.validateTurnir(naziv, brRundi, vrem, pocetak, kraj);
            if (!validation.res){
                return reject(validation.msg);
            }
            let novi = new Turnir(null, naziv, drzava, pocetak, kraj, brRundi, vrem);
            fetch("https://localhost:5001/Turnir/DodajTurnir", {
                method: "POST",
                headers: {
                    "Content-Type":"application/json"
                },
                body: JSON.stringify({naziv: naziv, datumOd: pocetak,
                    datumDo: kraj, drzavaID: drzava, 
                    timeControl: vrem, brojRundi: brRundi
                })
            }).then(res => {
                if (res.ok){
                    res.json().then(inf => {
                        novi.id = inf.id;
                        console.log(novi);
                        return resolve(novi);
                    })
                }
                else 
                {
                    return reject("Doslo je do greske");
                }
            });   
        });
    }

    obrisiTurnir(){
        if(confirm(`Da li ste sigurni da zelite da obrisete turnir ${this.naziv}`)){
            fetch(`https://localhost:5001/Turnir/ObrisiTurnir/${this.id}`, {
                method: "DELETE"
            })
            .then(resp => {
                if(resp.ok) {
                    this.container.parentNode.removeChild(this.container);
                }
                else
                    alert("Doslo je do greske");
            })
        }
    }

    renewBody(cls){
        let tbody = this.container.querySelector(`.${cls}`);
        let table = tbody.parentNode;
        table.removeChild(tbody);
        return createElement("tbody", table, [cls]);
    }

    prikaziPartije(ev){
        if(!this.partijePreuzete) this.preuzmiPartije();
        let cont = this.container.querySelector(".contParticipants");
        let details = this.container.querySelector(".contDetails");
        let games = this.container.querySelector(".contGames");
        if (games.style.display==="none"){
            details.style.display="none";
            cont.style.display="none";
            ev.target.parentNode.querySelector("button.clicked")?.classList.remove("clicked");
            games.style.display="flex";
        }
        else {
            games.style.display="none";
        }
        buttonClicked(ev);
    }

    dodajPartiju(partija){
        Partija.dodajPartiju(partija)
            .then(msg => {
                alert(`${msg}`)
                this.partijePreuzete = false;
                this.preuzmiPartije();
            })
            .catch(msg => {
                alert(`${msg}`)
            });
    }

    prikaziUcesnike(ev){
        if(!this.ucesniciPreuzeti) { 
            this.preuzmiUcesnike();
        }

        let cont = this.container.querySelector(".contParticipants");
        let details = this.container.querySelector(".contDetails");
        let games = this.container.querySelector(".contGames");
        if (cont.style.display==="none") {
            details.style.display="none";
            games.style.display="none";
            cont.style.display="block";
            ev.target.parentNode.querySelector("button.clicked")?.classList.remove("clicked");
        }
        else {
            cont.style.display="none";
        }
        buttonClicked(ev);
    }

    dodajUcesnika(ucesnikID){
        Ucesnik.dodajUcesnika(this.id, ucesnikID)
        .then(() => {
            this.ucesniciPreuzeti = false;
            this.preuzmiUcesnike();
        })
        .catch((msg) => {
            console.log(msg);
        })
    }

    obradiUcesnike(){
        let dodajSelect = this.container.querySelector(".select-player");
        let beliSelect = this.container.querySelector(".beliSelect");
        let crniSelect = this.container.querySelector(".crniSelect");
        let option;
        dodajSelect.length = 0;
        beliSelect.length = 0;
        crniSelect.length = 0;
        this.ucesnici.forEach(uc => {
            option = createElInner("option", uc.ime + " " + uc.prezime, beliSelect);
            option.value = uc.id;
            option = createElInner("option", uc.ime + " " + uc.prezime, crniSelect);
            option.value = uc.id;
        })

        StranicaTurniri.igraci
            .filter(igr => !(this.ucesnici.map(uc => uc.id))
            .includes(igr.id)).forEach(el => {
                createElInner("option", el.ime, dodajSelect).value=el.id;
            })
    }

    async preuzmiUcesnike(){
        let ucesnik;
        this.ucesnici.length = 0;
        const body = this.renewBody("playersBody");
        return fetch(`https://localhost:5001/Turnir/PreuzmiUcesnike/${this.id}`)
            .then(p => p.json()
            .then(data => {
                data.forEach((uc) => {
                    ucesnik = new Ucesnik(uc.id, uc.titula, uc.ime, uc.prezime, uc.drzava, uc.mesto, uc.bodovi);
                    ucesnik.draw(body);
                    this.ucesnici.push(ucesnik);
                })
                this.obradiUcesnike();
                this.ucesniciPreuzeti=true;
            }));
    }

    preuzmiPartije(){
        const body = this.renewBody("gamesBody");
        let partija;
        fetch(`https://localhost:5001/Turnir/PreuzmiPartije/${this.id}`)
            .then(p => p.json())
            .then(data => {
                data.forEach((pa) => {
                    partija = new Partija(pa.beliIme, pa.beliPrezime, 
                        pa.crniIme, pa.crniPrezime, pa.ishod, pa.runda, 
                        pa.brPoteza, pa.notacija);
                    partija.draw(body);
                })
            });
        this.partijePreuzete=true;
    }

    swapView(ev){
        const detailsBlock = this.container.querySelector(".contDetails");
        const participantsBlock = this.container.querySelector(".contParticipants");
        const gamesBlock = this.container.querySelector(".contGames");
        if(ev.target.nodeName !== "BUTTON"){
            if (detailsBlock.style.display === "none"){
                participantsBlock.style.display = "none";
                gamesBlock.style.display = "none";
                detailsBlock.style.display = "block";
                this.container.querySelector(".controls.right > button.clicked")?.classList.remove("clicked");
            }
            else {
                detailsBlock.style.display = "none";
            }
        }
    }

    static validateTurnir(naziv, brRundi, vremKontrola, pocetak, kraj) {
        let trimedNaziv = naziv.trim();
        let trimedVremK = vremKontrola.trim();
        if (!trimedNaziv){
            return {
                res : false,
                msg : "Polje naziv ne moze biti prazno"
            };
        }
        if (trimedVremK.match(/(([\d]+)(\|[\d]+)?)?(\+[\d]+)?/)[0] != trimedVremK){
            return {
                res : false,
                msg : "Vremenska kontrola nije odgovarajuceg formata"
            }; 
        }
        if (!pocetak || !kraj) {
            return {
                res : false,
                msg : "Izaberite datum pocetka i okoncanja turnira"
            }; 
        }
        if (!brRundi || (brRundi <= 0 || brRundi >= 100)){
            return {
                res : false,
                msg : "Vrednost broja rundi mora biti izmedju 1 i 100"
            }; 
        }
        return {
            res : true,
            msg : "Validacija uspesna"
        };
    }
}