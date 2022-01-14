import { createElement, createElInner } from "./helpers.js";

export class Sahista {
    constructor(id, titula, ime, prezime, drzava, classical, blitz, rapid){
        this.id = id;
        this.titula = titula;
        this.ime = ime;
        this.prezime = prezime;
        this.drzava = drzava;
        this.classical = classical;
        this.blitz = blitz;
        this.rapid = rapid;
        this.container=null;
    }

    draw(parent){
        this.container = createElement("tr", parent);
        createElInner("td", this.titula, this.container);
        createElInner("td", this.ime, this.container);
        createElInner("td", this.prezime, this.container);
        createElInner("td", this.drzava, this.container);
        createElInner("td", this.classical, this.container);
        createElInner("td", this.rapid, this.container);
        createElInner("td", this.blitz, this.container);
    }
}