import { createElement, createElInner } from "./helpers.js";
import { Sahista } from "./sahista.js";

export class StranicaSahisti {
    constructor(pageCont){
        this.pageCont = pageCont;
        this.contSahisti = createElement("div", pageCont, ["contIgraci"]);
        this.sahisti = [];
        this.crtajHeader();
    }

    async fetch(){
        let igr;
        const body = this.renewBody("playersBody");
        return fetch("https://localhost:5001/Igrac/Preuzmi")
            .then(d => {
                d.json().then(igraci => {
                    igraci.forEach(i => {
                        igr = new Sahista(
                            i.id,
                            i.titula, 
                            i.ime, 
                            i.prezime,
                            i.drzava,
                            i.classical,
                            i.blitz,
                            i.rapid);
                        this.sahisti.push(igr);
                        igr.draw(body);
                    });
                });
            });
    }

    renewBody(cls){
        let tbody = this.contSahisti.querySelector(`.${cls}`);
        let table = tbody.parentNode;
        table.removeChild(tbody);
        return createElement("tbody", table, [cls]);
    }

    crtajHeader(){
        const tableCont = createElement("div", this.contSahisti, ["tabelaCont"]);
        const tabela = createElement("table", tableCont, ["igraciTable"]);
        const thead = createElement("thead", tabela);
        const kolone = ["Titula", "Ime", "Prezime", "Drzava", "Classical", "Rapid", "Blitz"];
        kolone.forEach(kolona => {
            createElInner("th", kolona, thead);
        });
        createElement("tbody", tabela, ["playersBody"]);
    }
}