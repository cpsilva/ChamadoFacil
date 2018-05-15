import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
import { AuthenticationModel } from '../../shared/models/authentication.model';
import { AuthenticationService } from '../../shared/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  model = new AuthenticationModel();

  constructor(
    private authenticationSerivce: AuthenticationService
  ) { }

  ngOnInit() {
  }

  login() {
    this.authenticationSerivce.authenticate(this.model);
  }
}
