import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Login } from 'src/app/models/login';
import { EventEmitterService } from 'src/app/services/event-emitter.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  login: Login;
  return: string = '';

  constructor(
    private sessions: SessionService,
    private router: Router,
    private route: ActivatedRoute,
    private usuarioService: UsuarioService,
    private eventEmitterService: EventEmitterService
  ) {
    this.login = new Login();
    this.eventEmitterService.refresh();
  }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => this.return = params['return'] || '/home');
  }

  onSubmit(): void {
    this.usuarioService.login(this.login)
      .then(x => {
        this.sessions.setLogin(x.response);
        this.eventEmitterService.refresh();
        this.router.navigateByUrl(this.return);
      })
  }
}
