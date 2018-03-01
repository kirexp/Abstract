import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeRoutingModule } from './employee.routing.module';
import { MainPageComponent } from './main-page/main-page.component';
import { HttpClient } from '@angular/common/http';
import { Remote } from '../../services/http-client';
import { AuthManager } from '../../services/auth-manager';
import { Router } from '@angular/router';
@NgModule({
  imports: [
    CommonModule,
    EmployeeRoutingModule,
  ],
  declarations: [MainPageComponent],
  bootstrap:[MainPageComponent],
  providers:[HttpClient,Remote,AuthManager]
})
export class EmployeeModule{ }
