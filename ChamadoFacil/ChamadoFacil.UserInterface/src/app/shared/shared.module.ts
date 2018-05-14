import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InterceptorModule } from './interceptor/interceptor.module';

@NgModule({
  imports: [
    CommonModule,
    InterceptorModule
  ],
  exports: [
    InterceptorModule
  ],
  declarations: []
})
export class SharedModule { }
