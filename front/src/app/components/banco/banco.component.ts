import { Component, OnInit } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { Login } from 'src/app/models/login';
import { RelatorioPonto } from 'src/app/models/relatorioPonto';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-banco',
  templateUrl: './banco.component.html',
  styleUrls: ['./banco.component.scss']
})
export class BancoComponent implements OnInit {

  login: Login;
  relatorio: RelatorioPonto;

  constructor(
    private pontoService: PontoService,
    private sessionService: SessionService
  ) {
    this.login = this.sessionService.getLogin();
  }

  ngOnInit(): void {
    this.pontoService.geraRelatorio(this.login.id)
      .then((data) => {
        this.relatorio = new RelatorioPonto(data.response);
      });
  }
}
