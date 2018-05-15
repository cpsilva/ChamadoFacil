import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterceptorModule } from './interceptor/interceptor.module';
import { ServicesModule } from './services/services.module';
import { GuardsModule } from './guards/guards.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    InterceptorModule,
    ServicesModule,
    GuardsModule,
  ],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    InterceptorModule,
    GuardsModule,
  ],
  declarations: []
})
export class SharedModule { }
