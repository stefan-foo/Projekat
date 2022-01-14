export class Potez {
    constructor(figura, saPolja, naPolje, info){
        if(!figura || figura === '') this.figura = "P";
        else this.figura = figura;
        this.saPolja = saPolja;
        this.naPolje = naPolje;
        this.info = info;
    }
}