import { GenericModel } from './genericModel';
import { Usuario } from './usuario';

export class Ponto extends GenericModel {
    data: Date;
    tarefa: string;

    usuarioId: number;
    usuario: Usuario;

    constructor(item?) {
        super(item);

        if (item) {
            this.data = new Date(item.data);
            this.tarefa = item.tarefa;
            this.usuarioId = item.usuarioId;

            if (item.usuario)
                this.usuario = new Usuario(item.usuario);
        }
    }
}
