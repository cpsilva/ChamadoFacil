import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DropdownListComponent } from './dropdown-list/dropdown-list.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
    DropdownListComponent
  ],
  exports: [
    DropdownListComponent
  ]
})
export class ComponentsModule { }
