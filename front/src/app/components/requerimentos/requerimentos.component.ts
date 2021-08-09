import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { RequerimentoService } from 'src/app/api-services/requerimento.service';
import { Login } from 'src/app/models/login';
import { Requerimento } from 'src/app/models/requerimento';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-requerimentos',
  templateUrl: './requerimentos.component.html',
  styleUrls: ['./requerimentos.component.scss']
})
export class RequerimentosComponent implements OnInit {

  login: Login;
  requerimentos: Requerimento[];

  constructor(
    private pontoService: PontoService,
    private requerimentoService: RequerimentoService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngOnInit(): void {

    var params = new HttpParams()
      .set("OrdenaPor", "Id")
      .set("OrdenacaoAsc", "false")
      .set("Includes", "Ponto")
      .set('UsuarioId', this.login.id.toString());

    this.requerimentoService.get({ params })
      .then((data: any) => {
        this.requerimentos = data.listaItens.map(x => new Requerimento(x));
      })
      .catch(() => {
      });
  }

}
