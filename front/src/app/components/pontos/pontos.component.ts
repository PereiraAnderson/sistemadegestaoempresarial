import { HttpParams } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
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

  constructor(
    private pontoService: PontoService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngAfterViewInit() {
    this.get();
  }

  get() {
    this.isLoadingResults = true;

    const params = new HttpParams()
      .set("OrdenaPor", "Data")
      .set("OrdenacaoAsc", "false")
      .set('UsuarioId', this.login.id.toString());

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

    this.pontoService.geraRelatorio(this.login.id);
  }

  delete(id: number) {
    this.pontoService.delete(id)
      .then(() => this.get());
  }
}
