import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Login } from 'src/app/models/login';
import { Usuario } from 'src/app/models/usuario';
import { SessionService } from 'src/app/services/session.service';
import { UtilService } from 'src/app/services/util.service';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.scss']
})
export class UsuarioComponent implements OnInit {
  usuario: Usuario;
  acao: string;
  perfis: string[];
  login: Login;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usuarioService: UsuarioService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
    this.usuario = new Usuario();
  }

  ngOnInit() {
    this.perfis = UtilService.getValuesEnumUsuarioPerfil();
    if (this.login) {
      this.usuarioService.getById(this.login.id)
        .then((data) => {
          this.usuario = new Usuario(data)
        });
      this.acao = 'Editar';
    }
    else {
      this.acao = 'Criar';
    }
  }

  onSubmit() {
    this.usuarioService.save(this.usuario)
      .then(() => this.router.navigate(['/usuarios']));
  }
}
