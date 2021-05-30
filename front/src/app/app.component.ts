import { Component, OnInit } from '@angular/core';
import { EnumUsuarioPerfil } from './models/enums/enumUsuarioPerfil';
import { EventEmitterService } from './services/event-emitter.service';
import { SessionService } from './services/session.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  login: boolean;
  perfil: EnumUsuarioPerfil;

  constructor(
    private sessions: SessionService,
    private eventEmitterService: EventEmitterService
  ) {
    if (this.eventEmitterService.subsVar == undefined) {
      this.eventEmitterService.subsVar = this.eventEmitterService.
        appRefresh.subscribe((name: string) => {
          this.ngOnInit();
        });
    }
  }

  ngOnInit(): void {
    this.setVariables();
  }

  setVariables() {
    this.login = this.sessions.getLogin() ? true : false;
    if (this.login)
      this.perfil = this.sessions.getLogin().perfil;
  }
}
