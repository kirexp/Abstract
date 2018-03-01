import { Component } from '@angular/core';
import { AuthManager } from './services/auth-manager';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private authManager:AuthManager) {
  }
  SignOut(){
    this.authManager.LogOut();
  }
}
