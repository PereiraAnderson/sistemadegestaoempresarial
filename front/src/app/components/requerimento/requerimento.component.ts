import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PontoService } from 'src/app/api-services/ponto.service';
import { RequerimentoService } from 'src/app/api-services/requerimento.service';
import { EnumRequerimentoStatus } from 'src/app/models/enums/enumRequerimentoStatus';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
import { Requerimento } from 'src/app/models/requerimento';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-requerimento',
  templateUrl: './requerimento.component.html',
  styleUrls: ['./requerimento.component.scss']
})
export class RequerimentoComponent implements OnInit {

  login: Login;
  pontos: Ponto[];
  pontoSelecionado: Ponto;
  requerimento: Requerimento;

  constructor(
    private router: Router,
    private pontoService: PontoService,
    private requerimentoService: RequerimentoService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
    this.requerimento = new Requerimento();
  }

  ngOnInit(): void {
    this.getPontos();
  }


  getPontos() {
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

  onSubmit() {
    const dt = new Date();
    this.requerimento.status = EnumRequerimentoStatus.Pendente;

    this.requerimento.usuarioId = this.login.id;
    this.requerimento.pontoId = this.pontoSelecionado.id;

    this.requerimentoService.save(this.requerimento)
      .then(() => this.router.navigate(['/requerimentos']));
  }

}
