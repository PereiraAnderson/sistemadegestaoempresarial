import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { EnumUsuarioPerfil } from 'src/app/models/enums/enumUsuarioPerfil';
import { Usuario } from 'src/app/models/usuario';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.scss']
})
export class CadastroUsuarioComponent implements OnInit {
  usuario: Usuario;
  acao: string;

  constructor(
    private router: Router,
    private usuarioService: UsuarioService
  ) {
    this.usuario = new Usuario();
  }

  ngOnInit() {

  }

  onSubmit() {
    this.usuario.perfil = EnumUsuarioPerfil.FUNCIONARIO;
    this.usuarioService.save(this.usuario)
      .then(() => this.router.navigate(['/login']));
  }
}
