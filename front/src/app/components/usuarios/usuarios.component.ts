import { AfterViewInit, Component } from '@angular/core';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Login } from 'src/app/models/login';
import { Usuario } from 'src/app/models/usuario';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent implements AfterViewInit {

  displayedColumns: string[] = ['nome', 'telefone', 'email', 'acoes'];
  data: Usuario[] = [];
  login: Login;

  isLoadingResults = true;
  isError = false;

  constructor(private usuarioService: UsuarioService,
    private sessionService: SessionService) { 
      this.login = this.sessionService.getLogin();
    }

  ngAfterViewInit() {
    this.get();
  }

  get() {
    this.isLoadingResults = true;
    this.usuarioService.get()
      .then((data: any) => {
        this.isLoadingResults = false;
        this.isError = false;
        this.data = data.listaItens.map(x => new Usuario(x));
      })
      .catch(() => {
        this.isLoadingResults = false;
        this.isError = true;
      });
  }

  delete(id: number) {
    this.usuarioService.delete(id)
      .then(() => this.get());
  }
}
