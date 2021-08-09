import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GenericService } from "./generic";

@Injectable({
    providedIn: 'root'
})

export class RequerimentoService extends GenericService {

    constructor(http: HttpClient) {
        super(http, 'Requerimento');
    }
}