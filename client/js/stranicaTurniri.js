import { createElement, createElInner, crtajFormu } from "./helpers.js";
import { Turnir } from "./turnir.js";

export class StranicaTurniri {
    static igraci = [];
    constructor(pageCont){
        this.pageCont = pageCont;
        this.header = createElement("div", pageCont, ["page-header"]);
        this.contTurniri = createElement("div", pageCont, ["contTurniri"]);
        this.crtajHeader();
    }

    async fetch(){
        let f;
        await StranicaTurniri.fetchCommon();
        return fetch("https://localhost:5001/Turnir/PreuzmiTurnire")
            .then(p =>{
                p.json().then(turniri => {
                    turniri.forEach(tr => {
                        f = new Turnir(tr.id, tr.naziv, tr.drzava, tr.datumOd, tr.datumDo, tr.brojRundi, tr.timeControl);
                        f.draw(this.contTurniri);
                    });
                })
        });
    }

    crtajHeader(){
        const opcije = createElInner("button", "Opcije ↓", this.header, ["headerBtn"]);
        const contForma = createElement("div", this.header, ["contForma"]);
        contForma.style.display="none";
        opcije.onclick = (ev) => {
            if (contForma.style.display == "none")
                contForma.style.display = "flex";
            else 
                contForma.style.display = "none";
        }
        crtajFormu(contForma);
        let btn = contForma.querySelector("button");
        btn.innerHTML="Dodaj turnir";
        btn.onclick = (ev) => this.dodajTurnir(contForma);
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
            }).catch((e) => {
                alert(e);
        });
    }

    static async fetchCommon(){
        return new Promise(async (resolve, reject) => {
            let res = await fetch("https://localhost:5001/Igrac/PreuzmiOsnovno");
            if (res.ok){
                StranicaTurniri.igraci = await res.json();
                resolve("Uspesno pribavljene");
            }
            else {
                reject("Doslo je do greske");
            }
        });
    }
}