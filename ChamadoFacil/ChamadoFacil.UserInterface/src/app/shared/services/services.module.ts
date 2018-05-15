import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from './application.service';
import { ConverterService } from './converter.service';
import { AuthenticationService } from './authentication.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule
  ],
  declarations: [],
  providers: [
    ApplicationService,
    ConverterService,
    AuthenticationService
  ]
})
export class ServicesModule { }
