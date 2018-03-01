import { AuthintithicationResult } from "../models/User";
import { Observable } from "rxjs/Observable";

export interface ISignInManager{
    Authintithicate(userNmae:string,password:string):Observable<AuthintithicationResult>;
    SetCredentials(jwt:string);
    Verify():AuthintithicationResult;
    ClearStorage();
    IsExpired();
    LogOut():AuthintithicationResult;
}
