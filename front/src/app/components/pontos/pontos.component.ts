import { HttpParams } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { Router } from '@angular/router';
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
export class PontosComponent implements AfterViewInit {

  displayedColumns: string[] = ['data', 'acoes'];
  data: Ponto[] = [];

  isLoadingResults = true;
  isError = false;

  login: Login;
  relatorio: RelatorioPonto;

  constructor(
    private pontoService: PontoService,
    private sessionService: SessionService,
    private router: Router
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngAfterViewInit() {
    this.get();
  }

  get() {
    this.isLoadingResults = true;

    var params = new HttpParams()
      .set("OrdenaPor", "Data")
      .set("OrdenacaoAsc", "false")
      .set('UsuarioId', '5');

    if (this.router.url.endsWith("ponto"))
      params = params.set('Hoje', 'true');

    this.pontoService.get({ params })
      .then((data: any) => {
        this.isLoadingResults = false;
        this.isError = false;
        this.data = data.listaItens.map(x => new Ponto(x));
      })
      .catch(() => {
        this.isLoadingResults = false;
        this.isError = true;
      });

    this.pontoService.geraRelatorio(5)
      .then((data) => {
        debugger;
        this.relatorio = data.response;
      });
  }

  delete(id: number) {
    this.pontoService.delete(id)
      .then(() => this.get());
  }
}
