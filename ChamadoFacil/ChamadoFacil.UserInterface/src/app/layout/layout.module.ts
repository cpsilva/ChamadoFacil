import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../routing/routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    AppRoutingModule
  ],
  declarations: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent
  ]
})
export class LayoutModule { }
