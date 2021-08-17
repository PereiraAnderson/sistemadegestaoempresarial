import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { UsuarioService } from 'src/app/api-services/usuario.service';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
import { RelatorioPonto } from 'src/app/models/relatorioPonto';
import { Usuario } from 'src/app/models/usuario';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-pontos',
  templateUrl: './pontos.component.html',
  styleUrls: ['./pontos.component.scss']
})
export class PontosComponent implements OnInit {

  pontos: Ponto[] = [];
  usuarios: Usuario[] = [];
  usuarioSelecionado: Usuario;

  hora: string;
  edit: boolean;

  login: Login;
  relatorio: RelatorioPonto;

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

  getPontos() {
    var params = new HttpParams()
      .set("OrdenaPor", "Data")
      .set("OrdenacaoAsc", "false")

    if (this.login.perfil != 1)
      params = params
        .set('UsuarioId', this.login.id.toString());
    else if (this.usuarioSelecionado)
      params = params
        .set('UsuarioId', this.usuarioSelecionado.id.toString());

    this.pontoService.get({ params })
      .then((data: any) => {
        this.pontos = data.listaItens.map(x => new Ponto(x));
      })
      .catch(() => {
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

  update(id: number) {
    let ponto = this.pontos.find(x => x.id == id);
    var split = this.hora.split(':');
    ponto.data = new Date(ponto.data.setHours(+split[0], +split[1]));

    this.pontoService.save(ponto)
      .then(() => this.getPontos());
  }

  delete(id: number) {
    this.pontoService.delete(id)
      .then(() => this.getPontos());
  }
}
