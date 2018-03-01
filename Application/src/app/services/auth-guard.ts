import { CanActivate } from "@angular/router/src/interfaces";
import { ActivatedRouteSnapshot, RouterStateSnapshot,Router } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { AuthManager } from "./auth-manager";
import { Injectable } from "@angular/core";
@Injectable()
export class AuthGuard implements CanActivate{
    constructor(private authService: AuthManager,private router:Router) {

    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
      var allow= this.authService.Verify();
      if(allow){
          return true;
      }else{
        this.router.navigateByUrl('/auth-page')
      }
    }
    
} 