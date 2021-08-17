import { PontoNormalizado } from "./pontoNormalizado";

export class RelatorioPonto {
    usuario: string;
    pontos: PontoNormalizado[];
    saldoString: string;
    pagamento: number;

    constructor(item?) {
        if (item) {
            this.usuario = item.usuario;
            this.pontos = item.pontos.map(x => new PontoNormalizado(x));
            this.saldoString = item.saldoString;
            this.pagamento = item.pagamento;
        }
    }
}
