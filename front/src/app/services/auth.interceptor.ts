import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { SessionService } from './session.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private sessionService: SessionService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //const url = request.url;
    //if (!url.match('/Login') && !url.match('/Usuario'))
    var token = this.sessionService.getToken();

    if (token)
      request = request.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          'Accept': 'application/json',
          'Authorization': `Bearer ${token}`,
        },
      });

    return next.handle(request);
  }
}
