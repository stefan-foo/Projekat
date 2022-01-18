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