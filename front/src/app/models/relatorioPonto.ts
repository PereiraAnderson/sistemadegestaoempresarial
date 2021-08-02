import { PontoNormalizado } from "./pontoNormalizado";

export class RelatorioPonto {
    usuario: string;
    pontos: PontoNormalizado[];
    saldo: Date;

    constructor(item?) {
        if (item) {
            this.usuario = item.usuario;
            this.pontos = item.pontos.map(x => new PontoNormalizado(x));
            this.saldo = item.saldo;
        }
    }
}
