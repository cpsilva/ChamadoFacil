import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class CustomHttpInterceptor implements HttpInterceptor {
  constructor(private readonly authenticationService: AuthenticationService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    let url = request.url;

    if (!url.startsWith('http')) {
      url = document.getElementsByTagName('base').item(0).href + url;
    }

    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.authenticationService.getToken()}`
      },
      url
    });

    return next.handle(request);
  }
}
