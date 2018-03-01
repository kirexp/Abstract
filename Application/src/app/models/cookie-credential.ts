import { User, AuthintithicationResult } from "../models/User";
import { ISignInManager } from "../interfaces/iSignInManager";
import { Observable } from "rxjs/Observable";

export class CookiesSession implements ISignInManager{
    LogOut(): AuthintithicationResult {
        throw new Error("Method not implemented.");
    }
    Authintithicate(userNmae: string, password: string): Observable<AuthintithicationResult> {
        throw new Error("Method not implemented.");
    }
    ClearStorage() {
        throw new Error("Method not implemented.");
    }
    IsExpired() {
        throw new Error("Method not implemented.");
    }
    SetCredentials(jwt:string) {
        throw new Error("Method not implemented.");
    }
    Verify(): AuthintithicationResult {
        throw new Error("Method not implemented.");
    }

}

