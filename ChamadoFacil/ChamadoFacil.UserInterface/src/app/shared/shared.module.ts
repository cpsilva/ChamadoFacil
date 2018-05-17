import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterceptorModule } from './interceptor/interceptor.module';
import { ServicesModule } from './services/services.module';
import { GuardsModule } from './guards/guards.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ComponentsModule } from './components/components.module';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    InterceptorModule,
    ServicesModule,
    GuardsModule,
    ComponentsModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    InterceptorModule,
    GuardsModule,
    ComponentsModule
  ],
  declarations: []
})
export class SharedModule { }
