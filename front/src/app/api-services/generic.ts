import { urlApi } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

export class GenericService {

    protected controllerUrl: string;

    constructor(
        protected http: HttpClient,
        protected API_URL: string) {
        this.controllerUrl = `${urlApi}${this.API_URL}`;
    }

    get = (options?): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.get(`${this.controllerUrl}`, options)
                .toPromise()
                .then((response) => {
                    resolve(response);
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });


    getById = (id, options?): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.get(`${this.controllerUrl}/${id}`, options)
                .toPromise()
                .then((response) => {
                    resolve(response);
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });

    delete = (id): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.delete(`${this.controllerUrl}/${id}`)
                .toPromise()
                .then((response) => {
                    resolve(response);
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });

    private post = (data: any): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.post(`${this.controllerUrl}`, data)
                .toPromise()
                .then((response) => {
                    resolve(response);
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });

    private put = (data: any): Promise<any> =>
        new Promise((resolve, reject) => {
            this.http.put(`${this.controllerUrl}`, data)
                .toPromise()
                .then((response) => {
                    resolve(response);
                })
                .catch((err) => {
                    console.error(err)
                    reject(err);
                })
        });

    save(data: any): Promise<any> {
        if (data.id)
            return this.put(data)
        else
            return this.post(data);
    }
}
