import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PontoService } from 'src/app/api-services/ponto.service';
import { EnumUsuarioPerfil } from 'src/app/models/enums/enumUsuarioPerfil';
import { Login } from 'src/app/models/login';
import { Ponto } from 'src/app/models/ponto';
import { SessionService } from 'src/app/services/session.service';
import { ThemePalette } from '@angular/material/core';

@Component({
  selector: 'app-ponto',
  templateUrl: './ponto.component.html',
  styleUrls: ['./ponto.component.scss']
})
export class PontoComponent implements OnInit {
  ponto: Ponto;
  acao: string;
  login: Login;
  agora: Date;

  color: ThemePalette = 'primary';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pontoService: PontoService,
    private sessionService: SessionService) {
    this.login = this.sessionService.getLogin();
    this.ponto = new Ponto();
    setInterval(() => {
      this.agora = new Date();
    }, 1000);
  }

  ngOnInit() {
    this.route.params.subscribe((params: Params) => {
      const id = params['id'];
      if (id != 0) {
        this.pontoService.getById(id)
          .then((data) => {
            this.ponto = new Ponto(data);
          });
        this.acao = 'Editar';
      }
      else {
        this.ponto = new Ponto();
        this.ponto.data = new Date();
        this.onSubmit();
      }
    });
  }

  onSubmit() {
    const dt = new Date();
    this.ponto.data = dt.toISOString();

    if (this.login.perfil != EnumUsuarioPerfil.SGE)
      this.ponto.usuarioId = this.login.id;

    this.pontoService.save(this.ponto)
      .then(() => this.router.navigate(['/pontos']));
  }
}
