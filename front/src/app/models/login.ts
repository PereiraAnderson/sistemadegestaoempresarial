export class Login {
    email: string;
    senha: string;

    id: string;
    nome: string;
    perfil: number;
    token: string;

    constructor(item?) {
        if (item) {
            this.id = item.id;
            this.nome = item.nome;
            this.perfil = item.perfil;
            this.token = item.bearer;
        }
    }
}
