export class PontoNormalizado {
    entrada: Date;
    saida: Date;
    saldo: Date;
    tarefa: string;

    constructor(item?) {
        if (item) {
            this.entrada = item.entrada;
            this.saida = item.saida;
            this.saldo = item.saldo;
            this.tarefa = item.tarefa;
        }
    }
}
