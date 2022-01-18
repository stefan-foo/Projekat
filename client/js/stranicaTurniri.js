import { createElement, createElInner, createInputForm } from "./helpers.js";
import { Turnir } from "./turnir.js";

export class StranicaTurniri {
    static igraci = [];
    static drzave = [];
    constructor(pageCont){
        this.pageCont = pageCont;
        this.header = createElement("div", pageCont, ["page-header"]);
        this.contTurniri = createElement("div", pageCont, ["contTurniri"]);
        this.commonFetched = false;
        this.turniri = [];
        this.crtajHeader();
    }

    async fetch(searchCriteria){
        let f;
        this.detach();
        return fetch(`https://localhost:5001/Turnir/PreuzmiTurnire/${searchCriteria}`)
            .then(p =>{
                if(p.ok) {
                    p.json().then(turniri => {
                        turniri.forEach(tr => {
                            f = new Turnir(tr.id, tr.naziv, tr.drzavaID, tr.drzava, tr.datumOd, tr.datumDo, tr.brojRundi, tr.timeControl);
                            this.turniri.push(f);
                        });
                        this.show();
                    })
                }
                else {
                    console.log("not found");
                }
        }).catch(() => {
            console.log("not found");
        })
    }

    show(){
        this.turniri.forEach(el => {
            el.draw(this.contTurniri);
        })
    }

    detach(){
        while(this.turniri.length > 0){
            this.turniri.pop().detach();
        }
    }

    crtajHeader(){
        const searchCont = createElement("div", this.header, ["search-cont"]);
        const searchInput = createElement("input", searchCont, ["search-input"]);
        searchInput.placeholder = "naziv turnira";
        searchInput.addEventListener("keyup", (ev) => {
            if (ev.key === "Enter"){
                if (searchInput.value){
                    this.fetch(searchInput.value);
                    searchInput.value="";
                }
            }
        });
        searchInput.disabled=true;

        const opcije = createElInner("button", "Dodaj turnir", this.header, ["headerBtn"]);
        const contForma = createElement("div", this.header, ["contForma"]);
        contForma.style.display="none";
        opcije.disabled=true;

        StranicaTurniri.fetchCommon().then((res) => { 
            opcije.disabled=false; 
            searchInput.disabled=false;
            StranicaTurniri.crtajFormu(contForma);
            let btn = contForma.querySelector("button");
            btn.innerHTML="Dodaj";
            btn.onclick = (ev) => this.dodajTurnir(contForma);
        });

        opcije.onclick = (ev) => {
            if (contForma.style.display == "none") {
                contForma.style.display = "flex";
            }
            else 
                contForma.style.display = "none";
        }
    }

    dodajTurnir(contForma){
        const nazivValue = contForma.querySelector(".naziv").value;
        const pocetakValue = contForma.querySelector(".pocetak").value;
        const krajValue = contForma.querySelector(".kraj").value;
        const drzavaValue = contForma.querySelector("select").value;
        const vremKontrolaValue = contForma.querySelector(".vremenska-kontrola").value;
        const brRundiValue = contForma.querySelector(".broj-rundi").value;
    
        Turnir.dodajTurnir(nazivValue, pocetakValue, krajValue, 
            drzavaValue, vremKontrolaValue, brRundiValue)?.then(tur => {
                tur.draw(this.contTurniri);
                this.turniri.push(tur);
            }).catch((e) => {
                alert(e);
        });
    }

    static async fetchCommon(){
        return new Promise(async (resolve, reject) => {
            Promise.all([
                fetch("https://localhost:5001/Igrac/Preuzmi"),
                fetch("https://localhost:5001/Drzava/Preuzmi")])
                    .then(([i, d]) => {
                        Promise.all([i.json(), d.json()])
                        .then(([igraci, drzave]) => {
                            igraci.forEach(igrac => {
                                StranicaTurniri.igraci.push({id: igrac.id, ime: igrac.ime + " " + igrac.prezime});
                            });
                            StranicaTurniri.drzave = drzave;
                            resolve("Uspesno pribavljeni podaci");
                        })
                        .catch(msg => {
                            reject(msg);
                        })
                    })
                    .catch(msg => {
                        reject(msg);
                    })
        })
    }

    static crtajFormu(contForma){
        const forma = createElement("div", contForma, ["form"]);
    
        let lbl, input, row;
        input = createInputForm("Naziv: ", "text", forma, ["form-control"]);
        input.className="naziv";
    
        let datumi = ["Pocetak", "Kraj"];
        datumi.forEach(el => { 
            input = createInputForm(`${el}: `, "date", forma, ["form-control"]);
            input.className = el.toLowerCase();
        });
    
        row = createElement("div", forma, ["form-control"]);
        lbl = createElInner("label", "Drzava odrzavanja: ", row);
        lbl.type = "text";
        let select = createElement("select", row);

        StranicaTurniri.drzave.forEach(dr => {
            input = createElInner("option", dr.naziv, select);
            input.value = dr.drzavaID;
        })
    
        input = createInputForm("Broj rundi: ", "number", forma, ["form-control"]);
        input.className = 'broj-rundi';
        input = createInputForm("Vremenska kontrola: ", "text", forma, ["form-control"]);
        input.placeholder = '30|15+15';
        input.className = 'vremenska-kontrola';
    
        row = createElement("div", forma, ["form-control"]);
        createElement("button", row, ["submit-btn", "headerBtn"]);
    }
}