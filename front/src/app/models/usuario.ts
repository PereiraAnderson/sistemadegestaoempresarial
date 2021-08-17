import { EnumUsuarioPerfil } from './enums/enumUsuarioPerfil';
import { GenericModel } from './genericModel';

export class Usuario extends GenericModel {
    nome: string;
    cpf: string;
    email: string;
    perfil: EnumUsuarioPerfil;
    login: string;
    senha: string;
    salarioHora: number;

    constructor(item?) {
        super(item);

        if (item) {
            this.nome = item.nome;
            this.cpf = item.cpf;
            this.email = item.email;
            this.perfil = item.perfil;
            this.login = item.login;
            this.senha = item.senha;
            this.salarioHora = item.salarioHora;
        }
    }
}
