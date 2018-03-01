import { User, AuthResult, AuthintithicationResult } from "../models/User";
import { debug, error } from "util";
import { Inject } from "@angular/core";
import { LocalStorageSession } from "../models/local-storage.credential";
import { Remote } from "./http-client";
import { Router } from "@angular/router";
import 'rxjs/add/operator/map'
import { Observable } from "rxjs/Observable";
export class AuthManager{
    Identity:User;
    IsAuthenticated:boolean=false;
    JwtToken:string;
    constructor(@Inject(LocalStorageSession) private auth:LocalStorageSession) {
        let result = auth.Verify();
        if(result.IsSuccess){
            this.Identity=result.Identity;
            this.IsAuthenticated=true;
        }else{
            this.Identity=new User("");
        }
    }
    Authintithicate(userName:string,password:string):Observable<AuthintithicationResult>{
        let result = this.auth.Authintithicate(userName,password);
        return result.map((result)=>{
            if(result.IsSuccess){
                this.IsAuthenticated=true;
                this.Identity=result.Identity;
            }
            return result;
        });
    }
    Verify():boolean{
        let result = this.auth.Verify();
        if(result.IsSuccess){
            this.Identity.UserName=result.Identity.UserName;
        }
        return result.IsSuccess;
    }
    LogOut(){
       let result = this.auth.LogOut();
       if(result.IsSuccess){
        this.Identity=result.Identity;
        this.IsAuthenticated=false;
       }
    }
    HasClaim(claimName:string):boolean{
        return this.Identity.role.indexOf(claimName)>-1;
    }

}