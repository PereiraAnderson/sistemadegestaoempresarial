export class Login {
    login: string;
    senha: string;

    id: number;
    nome: string;
    perfil: number;

    constructor(item?) {
        if (item) {
            this.id = item.id;
            this.nome = item.nome;
            this.perfil = item.perfil;
        }
    }
}
