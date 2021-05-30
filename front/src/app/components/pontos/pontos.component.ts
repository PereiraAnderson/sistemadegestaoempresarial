import { AfterViewInit, Component } from '@angular/core';
import { PontoService } from 'src/app/api-services/ponto.service';
import { Ponto } from 'src/app/models/ponto';

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

  constructor(private pontoService: PontoService) { }

  ngAfterViewInit() {
    this.get();
  }

  get() {
    this.isLoadingResults = true;
    this.pontoService.get()
      .then((data: any) => {
        this.isLoadingResults = false;
        this.isError = false;
        this.data = data.listaItens.map(x => new Ponto(x));
      })
      .catch(() => {
        this.isLoadingResults = false;
        this.isError = true;
      });
  }

  delete(id: number) {
    this.pontoService.delete(id)
      .then(() => this.get());
  }
}
