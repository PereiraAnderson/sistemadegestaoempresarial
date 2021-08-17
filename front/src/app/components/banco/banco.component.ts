import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Login } from 'src/app/models/login';
import { RelatorioPonto } from 'src/app/models/relatorioPonto';
import { Usuario } from 'src/app/models/usuario';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-banco',
  templateUrl: './banco.component.html',
  styleUrls: ['./banco.component.scss']
})
export class BancoComponent implements OnInit {

  login: Login;
  relatorio: RelatorioPonto;

  usuarios: Usuario[] = [];
  usuarioSelecionado: Usuario;

  constructor(
    private pontoService: PontoService,
    private usuarioService: UsuarioService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngOnInit() {
    this.getUsuarios();
  }

  getPontos(): void {
    this.pontoService.geraRelatorio(this.usuarioSelecionado.id)
      .then((data) => {
        this.relatorio = new RelatorioPonto(data.response);
      });
  }

  getUsuarios() {
    var params = new HttpParams()
      .set("OrdenaPor", "Nome")
      .set("OrdenacaoAsc", "true");

    this.usuarioService.get({ params })
      .then((data: any) => {
        this.usuarios = data.listaItens.map(x => new Usuario(x));
        this.usuarioSelecionado = this.usuarios[0];
        this.getPontos();
      })
      .catch(() => {
      });
  }
}
