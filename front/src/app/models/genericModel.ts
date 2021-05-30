export class GenericModel {
    id: any;
    ativo: boolean;
    dataCriacao: Date;
    dataModificacao: Date;

    constructor(item?) {
        if (item) {
            this.id = item.id;
            this.ativo = item.ativo;
            this.dataCriacao = item.dataCriacao;
            this.dataModificacao = item.dataModificacao;
        }
    }
}
