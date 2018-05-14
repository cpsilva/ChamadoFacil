import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomHttpInterceptor } from './http.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptor, multi: true }
  ]
})
export class InterceptorModule { }
