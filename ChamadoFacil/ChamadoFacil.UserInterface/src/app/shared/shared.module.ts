import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterceptorModule } from './interceptor/interceptor.module';
import { ServicesModule } from './services/services.module';

@NgModule({
  imports: [
    CommonModule,
    InterceptorModule,
    ServicesModule
  ],
  exports: [
    InterceptorModule
  ],
  declarations: []
})
export class SharedModule { }
