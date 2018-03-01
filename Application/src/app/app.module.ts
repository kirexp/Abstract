import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms'
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { AuthManager } from './services/auth-manager';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AuthenticationInterceptor } from './services/intercepter';

import { AuthGuard } from './services/auth-guard';
import { LocalStorageSession } from './models/local-storage.credential';
import { AuthVariables } from './models/User';
import { Remote } from './services/http-client';
import { Router, RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UnAuthGuard } from './services/unauthorized-guard';
import { AuthPageComponent } from './shared-components/auth.page.component/auth-page.component';
import { RegistrationPageComponent } from './shared-components/registration-page/registration-page.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthPageComponent,
    RegistrationPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule.forRoot()
  ],
  providers: [
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthenticationInterceptor,
    multi: true,
  },AuthGuard,UnAuthGuard,LocalStorageSession,AuthManager,AuthVariables,Remote,HttpClientModule],
  bootstrap: [AppComponent]
})
export class AppModule { }