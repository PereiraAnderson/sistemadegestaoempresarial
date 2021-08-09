import { EnumRequerimentoStatus } from './enums/enumRequerimentoStatus';
import { GenericModel } from './genericModel';
import { Ponto } from './ponto';
import { Usuario } from './usuario';

export class Requerimento extends GenericModel {
    status: EnumRequerimentoStatus;
    statusString: string;
    justificativa: string;

    usuarioId: number;
    usuario: Usuario;

    pontoId: number;
    ponto: Ponto;

    constructor(item?) {
        super(item);

        if (item) {
            this.status = item.status;
            this.statusString = EnumRequerimentoStatus[this.status];
            this.justificativa = item.justificativa;
            this.usuarioId = item.usuarioId;
            this.pontoId = item.pontoId;

            if (item.usuario)
                this.usuario = new Usuario(item.usuario);

            if (item.ponto)
                this.ponto = new Ponto(item.ponto);
        }
    }
}
