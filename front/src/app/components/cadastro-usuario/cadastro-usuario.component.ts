import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { EnumUsuarioPerfil } from 'src/app/models/enums/enumUsuarioPerfil';
import { Login } from 'src/app/models/login';
import { Usuario } from 'src/app/models/usuario';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.scss']
})
export class CadastroUsuarioComponent implements OnInit {
  usuario: Usuario;
  acao: string;
  login: Login;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private usuarioService: UsuarioService,
    private sessionService: SessionService
  ) {
    this.usuario = new Usuario();
    this.login = this.sessionService.getLogin();
  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      const id = params['id'];
      if (id != 0) {
        this.usuarioService.getById(id)
          .then((data) => {
            this.usuario = new Usuario(data);
          });
        this.acao = 'Editar';
      }
      else {
        this.usuario = new Usuario();
        this.usuario.perfil = EnumUsuarioPerfil.FUNCIONARIO;
      }
    });
  }

  onSubmit() {
    this.usuario.salarioHora = +this.usuario.salarioHora;
    this.usuarioService.save(this.usuario)
      .then(() => this.router.navigate(['/usuarios']));
  }
}
