import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Usuario } from 'src/app/models/usuario';
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private usuarioService: UsuarioService) {

    this.usuario = new Usuario();
  }

  ngOnInit() {
    this.perfis = UtilService.getValuesEnumUsuarioPerfil();

    this.route.params.subscribe((params: Params) => {
      const id = params['id'];
      if (id != 0) {
        this.usuarioService.getById(id)
          .then((data) => {
            this.usuario = new Usuario(data)
          });
        this.acao = 'Editar';
      }
      else {
        this.acao = 'Criar';
      }
    });
  }

  onSubmit() {
    this.usuarioService.save(this.usuario)
      .then(() => this.router.navigate(['/usuarios']));
  }
}
