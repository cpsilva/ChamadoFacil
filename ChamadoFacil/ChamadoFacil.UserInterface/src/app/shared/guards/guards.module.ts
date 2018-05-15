import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationGuard } from './authentication.guard';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [AuthenticationGuard]
})
export class GuardsModule { }
