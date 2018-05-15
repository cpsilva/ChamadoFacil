import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-formulario',
  templateUrl: './formulario.component.html'
})
export class FormularioComponent implements OnInit {
  path: string;
  titulo: string;
  desabilitado: boolean;

  constructor(
    private route: ActivatedRoute
  ) {
    this.path = this.route.snapshot.children.toString();
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

    //if (this.path == 'editar' || this.path == 'visualizar') {
    //  this.selecionar(this.route.snapshot.params['id']);
    //}
  }

  public desabilitarTela() {
    this.desabilitado = true;
  }
}
