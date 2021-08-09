import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PontoService } from 'src/app/api-services/ponto.service';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
import { SessionService } from 'src/app/services/session.service';
import { ThemePalette } from '@angular/material/core';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-ponto',
  templateUrl: './ponto.component.html',
  styleUrls: ['./ponto.component.scss']
})
export class PontoComponent implements OnInit {
  ponto: Ponto;
  pontos: Ponto[];
  acao: string;
  login: Login;
  agora: Date;

  color: ThemePalette = 'primary';

  constructor(
    private router: Router,
    private pontoService: PontoService,
    private sessionService: SessionService) {
    this.agora = new Date();
    this.login = this.sessionService.getLogin();
    this.ponto = new Ponto();
    setInterval(() => {
      this.agora = new Date();
    }, 1000);
  }

  ngOnInit() {
    var params = new HttpParams()
      .set("OrdenaPor", "Data")
      .set("OrdenacaoAsc", "false")
      .set('UsuarioId', this.login.id.toString())
      .set('Hoje', 'true');

    this.pontoService.get({ params })
      .then((data: any) => {
        this.pontos = data.listaItens.map(x => new Ponto(x));
      })
      .catch(() => {
      });
  }

  onSubmit() {
    const dt = new Date();
    this.ponto.data = dt.toISOString();

    this.ponto.usuarioId = this.login.id;

    this.pontoService.save(this.ponto)
      .then(() => this.router.navigate(['/pontos']));
  }
}
