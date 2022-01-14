import { createElement, createElInner, buttonClicked } from "./helpers.js";
import { Potez } from "./potez.js";

const pomGrid={
    a8: 63, b8: 62, c8: 61, d8: 60, e8: 59, f8: 58, g8: 57, h8: 56,
    a7: 55, b7: 54, c7: 53, d7: 52, e7: 51, f7: 50, g7: 49, h7: 48,
    a6: 47, b6: 46, c6: 45, d6: 44, e6: 43, f6: 42, g6: 41, h6: 40,
    a5: 39, b5: 38, c5: 37, d5: 36, e5: 35, f5: 34, g5: 33, h5: 32,
    a4: 31, b4: 30, c4: 29, d4: 28, e4: 27, f4: 26, g4: 25, h4: 24,
    a3: 23, b3: 22, c3: 21, d3: 20, e3: 19, f3: 18, g3: 17, h3: 16,
    a2: 15, b2: 14, c2: 13, d2: 12, e2: 11, f2: 10, g2: 9,  h2: 8,
    a1: 7,  b1: 6,  c1: 5,  d1: 4,  e1: 3,  f1: 2,  g1: 1,  h1: 0,
}

const pomeraj={
    P: [7, 9, 8, 16],
    Q: [-7, -8, -9, -1, 1, 7, 8, 9],
    R: [-8, 8, -1, 1],
    B: [-7, 7, 9, -9],
    N: [15, 17, 6, 10, -15, -17, -6, -10]
}

const figuraMap={ P: 6, Q: 2, K: 1, R: 3, N: 5, B: 4}

export class Tabla {
    constructor(notacija){
        this.notacija=notacija;
        this.container=null;
        this.potezi=this.parse(notacija);
        this.undo=[];
        this.history=[[
            3, 5, 4, 1, 2, 4, 5, 3,
            6, 6, 6, 6, 6, 6, 6, 6,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0,
            12,12,12,12,12,12,12,12, 
            9,11,10,7,8,10,11,9,
          ]];
        this.cur=0;
    }

    draw(parent) {
        if(!this.renew(parent)) return;
        if(this.container) {
            parent.appendChild(this.container);
            return;
        }
        const genCont = createElement("div", parent);
        this.container = genCont;
        const contGame = createElement("div", genCont, ["cont-game"]);
        const board = createElement("div", contGame, ["board"]);
        this.drawControls(genCont);

        let square = null;
        let j = 0;
        for(let i = 63; i >= 0; i--){
            square = createElement("div", board, ["square"]);
            square.classList.add((i+j)%2 ? "light" : "dark");
            square.setAttribute("index", i);
            if (!(i % 8)) j++;
        }
        this.setPieces(this.cur);
    }

    setPieces(cur){
        let square;
        this.history[cur].forEach((sq,index) => {
            square= this.container.querySelector(`[index="${index}"]`);
            if(sq) square.innerHTML=`&#${9811+sq}`;
            else  square.innerHTML="";
        })
    }

    undoMove(){
        this.setPieces(--this.cur);
        if(this.cur == 0)
            this.container.querySelector(".undoBtn").disabled=true;
    }

    makeMove(){
        this.cur++;
        this.container.querySelector(".undoBtn").disabled=false;
        if (this.cur < this.history.length){
            this.setPieces(this.cur);
            return;
        }

        this.history.push([]);
        this.history[this.cur].push(...this.history[this.cur-1]);
        let potez;

        if(this.cur % 2) potez = this.parseMove(this.potezi[this.cur-1], this.cur-1, true);
        else potez = this.parseMove(this.potezi[this.cur-1], this.cur-1, false);

        let before = potez.saPolja;
        let after = potez.naPolje;
        this.history[this.cur][after]=this.history[this.cur][before];
        this.history[this.cur][before]=0;
        before = this.container.querySelector(`[index="${before}"]`);
        after = this.container.querySelector(`[index="${after}"]`);
        after.innerHTML= before.innerHTML;
        before.innerHTML="";
    }

    drawControls(parent){
        const cntCont = createElement("div", parent, ["cont-board-controls"]);
        let btn = createElInner("button", "<<", cntCont, ["undoBtn"]);
        btn.onclick=(ev)=>this.undoMove();
        btn.disabled=true;
        btn = createElInner("button", ">>", cntCont, ["redoBtn"]);
        btn.onclick=(ev)=>this.makeMove();
        btn = createElInner("button", "-", cntCont, ["hideBtn"]);
        btn.onclick=(ev)=>{ this.container.parentNode.style.display="none"; }
    }

    renew(container){
        if(container.hasChildNodes()){ 
            if(container.childNodes[0] != this.container) {
                container.removeChild(container.childNodes[0]);
                return true;
            }
                else return false;
        }
        return true;
    }

    parse(notacija){
        let cista = notacija
            .replace(/[+#]?/g, '')
            .replace(/(\d\. ?)/g, '')
            .replace(/ ?([1])(\/[12])?-([12])(\/[12])?/, '');
        let potezi = cista.split(' '); 
        return potezi;
    }

    parseMove(pot, cur, beli){
        let pom = pot.match(/([1-8]?[a-h]?)?([QKNRB])?(x)?[a-h]?[1-8]?([a-h][1-8])(QKNBR)?/)
        if(!pom) return;
        let potez = new Potez(pom[2], pom[1], pomGrid[pom[4]], pom[5]);
        let lookfor = figuraMap[potez.figura];
        let mlt = -1;
        if (!beli) {
            mlt = 1;
            lookfor += 6;
        }

        let proverava;
        if(potez.figura == "P"){
            if (pom[3]){
                for(let i = 0; i < 2; i++){
                    proverava = potez.naPolje+mlt*pomeraj.P[i];
                    if(this.history[cur][proverava] == lookfor)
                    {
                        potez.saPolja=proverava;
                        return potez;
                    }
                }
            }
            else if (this.history[cur][potez.naPolje+pomeraj.P[2]*mlt]){ 
                potez.saPolja=potez.naPolje+pomeraj.P[2];
            }
            else {
                potez.saPolja=potez.naPolje+pomeraj.P[3]*mlt;
            }
        }
        else {
            let tablaPolje;
            pomeraj[potez.figura].forEach((val) => {
                proverava = potez.naPolje;
                while(true){
                    proverava += val;
                    if(proverava < 0 || proverava > 63) break;
                    tablaPolje = this.history[cur][proverava];
                    if (lookfor == tablaPolje) {
                        potez.saPolja = proverava;
                        return potez;
                    }
                    if (potez.figura == 'N') break;

                    if(beli && tablaPolje < 7 && tablaPolje > 0) break;
                    else if (!beli && tablaPolje > 6) break;
                }
            });
        }
        return potez;
    }
}