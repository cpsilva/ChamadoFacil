import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { LayoutModule } from '../../layout/layout.module';
import { IndexComponent } from './index/index.component';
import { RouterModule } from '@angular/router';
import { ListagemComponent } from './listagem/listagem.component';
import { FormularioComponent } from './formulario/formulario.component';

@NgModule({
  imports: [
    CommonModule,
    LayoutModule,
    RouterModule
  ],
  declarations: [
    AdminComponent,
    IndexComponent,
    ListagemComponent,
    FormularioComponent
  ],
  exports: [
    AdminComponent,
    IndexComponent
  ]
})
export class AdminModule { }
