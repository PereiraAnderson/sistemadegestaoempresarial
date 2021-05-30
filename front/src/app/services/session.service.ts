import { Injectable } from '@angular/core';
import { LocalStorageService } from 'ngx-webstorage';
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  constructor(
    private localStorage: LocalStorageService
  ) { }

  setSessions = (key: string, value: any): any => this.localStorage.store(key, value);

  getSessions = (key: string): any => this.localStorage.retrieve(key);

  deleteSessions = (key: string): void => this.localStorage.clear(key);

  setLogin = (login: Login): void => this.localStorage.store('login', login);

  getLogin = (): Login => this.localStorage.retrieve('login');

  deleteLogin = (): void => this.localStorage.clear('login');

  getToken = (): string => this.localStorage.retrieve('login')?.token;

}
