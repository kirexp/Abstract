import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './services/auth-guard';
import { AppComponent } from './app.component';
import { UnAuthGuard } from './services/unauthorized-guard';
import { AuthPageComponent } from './shared-components/auth.page.component/auth-page.component';
import { RegistrationPageComponent } from './shared-components/registration-page/registration-page.component';


const routes: Routes = [
  {path:'auth-page',component:AuthPageComponent,canActivate:[UnAuthGuard]},
  {path:'employee',loadChildren: './modules/employee-module/employee.module#EmployeeModule',canActivate:[AuthGuard]},
  {path:'register',component:RegistrationPageComponent,canActivate:[UnAuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
