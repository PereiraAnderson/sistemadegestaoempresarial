import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
import { RelatorioPonto } from 'src/app/models/relatorioPonto';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-pontos',
  templateUrl: './pontos.component.html',
  styleUrls: ['./pontos.component.scss']
})
export class PontosComponent implements OnInit {

  pontos: Ponto[] = [];

  login: Login;
  relatorio: RelatorioPonto;

  constructor(
    private pontoService: PontoService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngOnInit() {
    this.get();
  }

  get() {
    var params = new HttpParams()
      .set("OrdenaPor", "Data")
      .set("OrdenacaoAsc", "false")
      .set('UsuarioId', this.login.id.toString());

    this.pontoService.get({ params })
      .then((data: any) => {
        this.pontos = data.listaItens.map(x => new Ponto(x));
      })
      .catch(() => {
      });
  }

  delete(id: number) {
    this.pontoService.delete(id)
      .then(() => this.get());
  }
}
