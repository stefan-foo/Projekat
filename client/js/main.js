import { createElement, createElInner } from "./helpers.js";
import { StranicaSahisti } from "./stranicaSahisti.js";
import { StranicaTurniri } from "./stranicaTurniri.js";


const bar = createElement("div", document.body, ["nav"]);
const turniriStr = createElement("div", document.body, ["page", "active-page"]);
const sahistiStr = createElement("div", document.body, ["page"]);
const pages = [turniriStr, sahistiStr]
createNav();
const turniri = new StranicaTurniri(turniriStr);
turniri.fetch();
const sahisti = new StranicaSahisti(sahistiStr);
sahisti.fetch();

function createNav(){
    const elements = createElement("ul", bar, ["nav-elementi"]);
    let img = createElement("img", elements, ["pawn-image"]);
    img.src = "images/pawn.png";
    let el = createElInner("li", "Turniri", elements, ["element", "turniri-str-btn", "clicked"]);
    el.onclick = (ev) => swap(ev, turniriStr);
    el = createElInner("li", "Igrači", elements, ["element", "igraci-str-btn"]);
    el.onclick = (ev) => swap(ev, sahistiStr);

    el = createElement("li", elements, ["prikaz"]);
    createElement("span", el, ["prikaz-dugme"]);
    el.onclick = (ev) => {
        elements.querySelectorAll(".element").forEach(el => {
            if(el.classList.contains("active"))
                el.classList.remove("active");
            else
                el.classList.add("active");
        })
    }
}

function swap(ev, show){
    if (ev.target.classList.contains("clicked")) return;
    bar.querySelector(".clicked")?.classList.remove("clicked");
    ev.target.classList.add("clicked");
    pages.forEach((page) => {
        if(page != show){
            page.classList.remove("active-page");
        }
    });
    show.classList.add("active-page");
}

