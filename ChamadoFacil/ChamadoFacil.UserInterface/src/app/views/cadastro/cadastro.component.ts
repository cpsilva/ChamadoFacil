import { Component } from '@angular/core';
import { ApplicationService } from '../../shared/services/application.service';
import { usuarioModel } from '../../shared/models/UsuarioModel';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html'
})
export class CadastroComponent {
  model = new usuarioModel();

  constructor(
    private applicationService: ApplicationService,
    private route: Router
  ) { }

  cadastrar() {
    this.applicationService.post('Cadastro', this.model).subscribe(result => {
      this.route.navigate(['/login']);
    });
  }
}
