import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from './application.service';
import { ConverterService } from './converter.service';
import { AuthenticationService } from './authentication.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    ApplicationService,
    ConverterService,
    AuthenticationService
  ]
})
export class ServicesModule { }
