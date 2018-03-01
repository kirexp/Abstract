import { ISignInManager } from "../interfaces/iSignInManager";
import { AuthintithicationResult, User, AuthVariables, AuthResult, UserDto } from "./User";
import { Inject } from "@angular/core";
import { Remote } from "../services/http-client";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/map'

export class LocalStorageSession implements ISignInManager{
    /**
     *
     */
    constructor(@Inject(AuthVariables) private constants:AuthVariables,@Inject(Remote)private remote: Remote) {

    }
    Authintithicate(userName: string, password: string):Observable<AuthintithicationResult>{
        let model={
            UserName:userName,
            Password:password
        }
        return this.remote.post('http://localhost:49432/Account/Authenticate',model)
        .map((response)=>{
          if(response.state==1){
            let authResult = response.data as AuthResult;
             this.SetCredentials(authResult.accessToken);
             return new AuthintithicationResult(true,new User(userName));
            }else{
                let authRes=new AuthintithicationResult(false,new User(''));
                authRes.AuthintithicationError=response.errorText;
                return authRes;
            }
        })
    }
    SetCredentials(jwt:string) {
        try{
            // var shim = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6IkpvaG4gRG9lIiwiQ2xhaW1zIjpbIkFkbWluIiwiU3VwZXJVc2VyIiwiRGVtaUdvZCJdfQ.I6zcYKvimTWl-xnUjUN_WjJyFbM5NbDEr0bp7sZaaZE";
            // jwt=shim;
            let jwtPayload=JSON.parse(atob(jwt.split('.')[1])) as UserDto; //
            if(jwtPayload.Type!="Client"){
                jwtPayload.role.map(x=>localStorage.setItem(x,'true'));
            }
           localStorage.setItem(this.constants.JWT,jwt);
           localStorage.setItem(this.constants.UserName,jwtPayload.unique_name);
           localStorage.setItem(this.constants.ExpirationDateTime,jwtPayload.exp.toString());
        }catch(ex){
            console.log('Error on setting credentials');
        }
    }
    Verify(): AuthintithicationResult {
        if(localStorage.getItem(this.constants.UserName)!=null&&!this.IsExpired()){
            return new AuthintithicationResult(true,new User(localStorage.getItem(this.constants.UserName)));
        }else{
            this.ClearStorage();
            return new AuthintithicationResult(false,new User(''));
        }
    }
    ClearStorage() {
        localStorage.removeItem(this.constants.UserName);
        localStorage.removeItem(this.constants.ExpirationDateTime);
        localStorage.removeItem('');
    }
    IsExpired() {
        let tokenExpirationDate =Date.parse(localStorage.getItem(this.constants.ExpirationDateTime));
        return Date.now()>tokenExpirationDate;
    }
    
    
    LogOut(): AuthintithicationResult {
        if(localStorage.getItem(this.constants.UserName)!=null)
        localStorage.removeItem(this.constants.UserName);
        return new AuthintithicationResult(true,new User(''));
    }
    
}