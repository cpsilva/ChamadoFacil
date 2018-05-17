import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApplicationService } from '../../../shared/services/application.service';
import { Router } from '@angular/router';
import { chamadoModel } from '../../../shared/models/ChamadoModel';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html'
})
export class FormularioComponent implements OnInit {
  path: string;
  titulo: string;
  desabilitado: boolean;
  id: number;
  model = new chamadoModel();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private applicationService: ApplicationService
  ) {
    this.path = this.route.snapshot.children.toString();
    this.id = this.route.snapshot.params['id']
    this.verificarRota();
  }

  ngOnInit() {
  }

  public verificarRota() {
    if (this.path.includes('visualizar')) {
      this.titulo = 'Visualizar';
      this.desabilitarTela();
    }

    if (this.path.includes('alterar')) {
      this.titulo = 'Alterar';
    }

    if (this.path == '') {
      this.titulo = 'Incluir';
    }

    if (this.path.includes('alterar') || this.path.includes('visualizar')) {
      this.selecionar(this.id);
    }
  }

  public desabilitarTela() {
    this.desabilitado = true;
  }

  salvar() {
    this.applicationService.post("Chamado", this.model).subscribe(result => {
      this.router.navigate(['/admin/listagem']);
    });
  }

  selecionar(id: number) {
    this.applicationService.get<chamadoModel>("Chamado", id).subscribe(result => {
      this.model = result;
    });
  }
}
