import { Injectable, EventEmitter } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';

@Injectable({
  providedIn: 'root'
})
export class EventEmitterService {

  appRefresh = new EventEmitter();
  subsVar: Subscription;

  constructor() { }

  refresh() {
    this.appRefresh.emit();
  }
}