export function createElement(type, parent=null, classes=[]){
    let el = document.createElement(type);
    if(classes.length > 0){
        classes.forEach(cl => {
            el.classList.add(cl);
        });
    }
    if (parent) parent.appendChild(el);
    return el;
}

export function createElInner(type, val, parent, cls) {
    let el = createElement(type, parent, cls);
    if (val) el.innerHTML=val;
    else el.innerHTML="/";
    return el;
}

export function createInputForm(lblval, type, parent, cls){
    let row = createElement("div", parent, cls);
    createElInner("label", lblval, row);
    let input = createElement("input", row);
    input.type=type;
    return input;
}

export function crtajFormu(contForma){
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
    fetch("https://localhost:5001/Drzava/Preuzmi")
        .then(p => {
            p.json().then(drzave => {
                drzave.forEach(dr => {
                    input = createElInner("option", dr.naziv, select);
                    input.value = dr.drzavaID;
                })
            });
    })
    input = createInputForm("Broj rundi: ", "number", forma, ["form-control"]);
    input.className = 'broj-rundi';
    input = createInputForm("Vremenska kontrola: ", "text", forma, ["form-control"]);
    input.placeholder = '30|15+15';
    input.className = 'vremenska-kontrola';

    createElement("button", forma, ["submit-btn", "headerBtn"]);
}

export function dateDisplayFormat(date){
    if (date instanceof Date) {
        return date.getFullYear() + "-" 
            + ('0' + (date.getMonth() + 1)).slice(-2) + "-"
            + ('0' + date.getDate()).slice(-2);
    }
}

export function buttonClicked(ev){
    if(ev.target.classList.contains("clicked"))
        ev.target.classList.remove("clicked");
    else
        ev.target.classList.add("clicked");
}