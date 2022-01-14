import { buttonClicked, createElInner } from "./helpers.js";
import { createElement } from "./helpers.js";
import { Tabla } from "./tabla.js";
export class Partija {
    constructor(bIme, bPrezime, cIme, cPrezime, ishod, runda, brPoteza, notacija){
        this.bIme=bIme;
        this.bPrezime=bPrezime;
        this.cIme=cIme;
        this.cPrezime=cPrezime;
        this.ishod=ishod;
        this.runda=runda;
        this.brPoteza=brPoteza;
        this.notacija=notacija;
        this.tabla=new Tabla(notacija);
        this.container=null;
    }

    draw(parent){
        if(!parent) return;
        const row = createElement("tr", parent);
        this.container = row;
        const beli = this.bIme[0]+". "+this.bPrezime;
        const crni = this.cIme[0]+". "+this.cPrezime;
        createElInner("td", `${beli} vs ${crni}`, row);
        createElInner("td", this.ishod, row);
        createElInner("td", this.runda, row);
        createElInner("td", this.brPoteza, row);
        const btn = createElInner("button", "Prikaz", createElement("td", row), ["headerBtn"]);
        btn.onclick = (ev) => {
            btn.parentNode.parentNode.parentNode
                .querySelector("button.clicked")?.classList.remove("clicked");
            buttonClicked(ev); 
            this.drawGame(parent.parentNode.parentNode.parentNode);
        }
    }

    drawGame(parent){
        const cont = parent.querySelector(".contGamesDisplay");
        cont.style.display="flex";
        this.tabla.draw(cont);
    }

    static dodajPartiju(partija){
        return new Promise((resolve, reject) => {
            let validacija = this.validirajPartiju(partija);
            if(!validacija.res){
                return reject(validacija.msg);
            }
            fetch("https://localhost:5001/Turnir/DodajPartiju", {
                method: "POST",
                headers: {
                    "Content-Type":"application/json"
                },
                body: JSON.stringify({
                    beliIgracID: partija.beliId, crniIgracId : partija.crniId,
                    ishod: partija.ishod, brojPoteza: partija.brPoteza,
                    runda: partija.runda, notacija: partija.notacija, 
                    turnirID : partija.turnir.id
                })
            }).then(res => {
                if (res.ok){
                    return resolve("Uspesno dodata partija");
                }
                else {
                    return reject("Doslo je do greske");
                }
                })
                .catch(() => {
                    return reject("Doslo je do greske");
                })
            })
    }

    static validirajPartiju(partija){
        if(partija.beliId == partija.crniId){
            return {
                res: false,
                msg: "Beli i crni igrac ne mogu imati istu vrednost"
            }
        }
        if(partija.turnir.brRundi < partija.runda){
            return {
                res: false,
                msg: "Neodgovarajuca vrednost runde" 
            }
        }
        if(!partija.notacija){
            return {
                res: false,
                msg: "Polje notacija mora biti popunjeno"
            }
        }
        return {
            res: true,
            msg: "Uspesna validacija"
        }
    }
}