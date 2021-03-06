import { Observable } from 'rxjs/Observable';
import {Injectable} from '@angular/core';
import {HttpEvent, HttpInterceptor, HttpHandler, HttpRequest} from '@angular/common/http';
import { AuthManager } from './auth-manager';
import { debug } from 'util';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
/**
 *
 */
    constructor(private authManager:AuthManager) {

    }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(this.authManager.JwtToken!=null){
      debugger
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authManager.JwtToken}`
        }
      });
 
      // req.headers.append('Authorization', 'Bearer '+this.authManager.JwtToken)
    }
    return next.handle(req);
  }
}