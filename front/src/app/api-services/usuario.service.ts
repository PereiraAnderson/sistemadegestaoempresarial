import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Login } from '../models/login';
import { GenericService } from "./generic";

@Injectable({
    providedIn: 'root'
})
export class UsuarioService extends GenericService {

    constructor(http: HttpClient) {
        super(http, 'Usuario');
    }

    login = (login: Login): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.post(`${this.controllerUrl}/Login`, login)
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
