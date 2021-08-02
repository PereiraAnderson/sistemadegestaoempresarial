import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GenericService } from "./generic";

@Injectable({
    providedIn: 'root'
})

export class PontoService extends GenericService {

    constructor(http: HttpClient) {
        super(http, 'Ponto');
    }

    geraRelatorio = (usuarioId: number): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.get(`${this.controllerUrl}/relatorio/${usuarioId}`)
                .toPromise()
                .then((response) => {
                    resolve({ response });
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });
}