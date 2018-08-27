import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  isAuthenticated = false;

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
  }
}
