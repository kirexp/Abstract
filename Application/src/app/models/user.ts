import { Injectable } from "@angular/core";
export class UserDto{
    unique_name:string;
    UserName:string;
    role:string[];
    Type:string;
    Email:string;
    exp:Date;
}
export class User{
    UserName:string;
    role:string[];
    Type:string;
    Email:string;

   constructor(userName:string) {
       this.UserName=userName;
   }
}
export class AuthintithicationResult{
    IsSuccess:boolean;
    Identity:User;
    AuthintithicationError:string;
    /**
     *
     */
    constructor(result:boolean,identity:User) {
        this.Identity=identity;
        this.IsSuccess=result;
    }
}
@Injectable()
export class AuthVariables{
    UserName:string; 
    JWT:string;
    ExpirationDateTime:string;
    /**
     *
     */
    constructor() {
        this.UserName="UserName"
        this.JWT="Auth-Token"
        this.ExpirationDateTime="ExpirationDateTime"
    }
}
export class AuthResult{
    accessToken:string;
    expiresIn:number;
    requestAt:Date;
    tokenType:string;
}