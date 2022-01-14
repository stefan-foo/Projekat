import { createElInner } from "./helpers.js";
import { createElement } from "./helpers.js";
export class Ucesnik {
    constructor(id, titula, ime, prezime, drzava, bodovi, mesto){
        this.id=id;
        this.titula=titula;
        this.ime=ime;
        this.prezime=prezime;
        this.drzava=drzava;
        this.mesto=mesto;
        this.bodovi=bodovi;
    }

    draw(parent){
        const row = createElement("tr", parent);
        createElInner("td", this.titula, row);
        createElInner("td", this.ime, row);
        createElInner("td", this.prezime, row);
        createElInner("td", this.drzava, row);
        createElInner("td", this.mesto, row);
        createElInner("td", this.bodovi, row);
    }

    static dodajUcesnika(turnirId, igracId){
        return new Promise((resolve, reject) => {
            fetch(`https://localhost:5001/Turnir/DodajUcesnika/${turnirId}/${igracId}`, {
                method: "post"
            }).then(res => {
                if (res.ok){
                    resolve("Ucesnik uspesno dodat");
                }
                else{
                    reject("Doslo je do greske");
                }
            })
            .catch(() => {
                reject("Doslo je do greske");
            });
        })
    }
}