export class PontoNormalizado {
    entrada: Date;
    saida: Date;
    jornadaString: string;
    tarefas: string;

    constructor(item?) {
        if (item) {
            this.entrada = new Date(item.entrada);
            this.saida = new Date(item.saida);
            this.jornadaString = item.jornadaString;
            this.tarefas = item.tarefas;
        }
    }
}
