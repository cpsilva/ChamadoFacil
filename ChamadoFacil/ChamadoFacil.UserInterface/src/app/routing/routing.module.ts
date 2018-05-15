import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { IndexComponent } from '../views/admin/index/index.component';
import { CadastroComponent } from '../views/cadastro/cadastro.component';
import { ListagemComponent } from '../views/admin/listagem/listagem.component';
import { FormularioComponent } from '../views/admin/formulario/formulario.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'cadastro',
    component: CadastroComponent,
  },

  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: IndexComponent },
      { path: 'listagem', component: ListagemComponent },
      {
        path: 'chamado', component: FormularioComponent, children: [
          { path: 'visualizar/:id', component: FormularioComponent },
          { path: 'alterar/:id', component: FormularioComponent }
        ]
      },
    ]
  },

  { path: '', component: LoginComponent, pathMatch: 'full' },

  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
