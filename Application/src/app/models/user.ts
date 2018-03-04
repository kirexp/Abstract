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
   static CreateInstance(userName,role,type,email):User{
    let user =new User(userName);
    user.role=role,
    user.Type=type,
    user.Email=email;
    return user;
   }

}
export class AuthintithicationResult{
    IsSuccess:boolean;
    Identity:User;
    AuthintithicationError:string;
    Token:string;
    /**
     *
     */
    constructor(result:boolean,identity:User,token) {
        this.Identity=identity;
        this.IsSuccess=result;
        this.Token=token;
    }
}
@Injectable()
export class AuthVariables{
    UserName:string; 
    JWT:string;
    ExpirationDateTime:string;
    Email:string;
    Type:string;
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