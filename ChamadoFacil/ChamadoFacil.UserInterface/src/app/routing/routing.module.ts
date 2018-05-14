import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { IndexComponent } from '../views/admin/index/index.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: 'admin',
    component: AdminComponent,
    children: [
      { path: '', component: IndexComponent },
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
