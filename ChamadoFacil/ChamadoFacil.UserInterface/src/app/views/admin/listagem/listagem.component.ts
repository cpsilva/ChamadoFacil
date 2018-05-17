import { Component, OnInit } from '@angular/core';
import { ApplicationService } from '../../../shared/services/application.service';
import { chamadoModel } from '../../../shared/models/ChamadoModel';

@Component({
  selector: 'app-listagem',
  templateUrl: './listagem.component.html'
})
export class ListagemComponent implements OnInit {
  model = new Array<chamadoModel>();

  constructor(
    private applicationService: ApplicationService
  ) { }

  ngOnInit() {
    this.listar();
  }

  listar() {
    this.applicationService.get<Array<chamadoModel>>("Listagem").subscribe(result => {
      this.model = result;
    });
  }
}
