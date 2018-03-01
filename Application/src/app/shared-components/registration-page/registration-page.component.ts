import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Remote } from '../../services/http-client';
import { AuthResult } from '../../models/User';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})
export class RegistrationPageComponent implements OnInit {
  public myForm:FormGroup;
  public authError:string="";
  constructor(private remote:Remote) {
    this.myForm=new FormGroup({
      userName:new FormControl('',[
        Validators.required, 
    ]),
      email:new FormControl('',[
      Validators.required,
      Validators.email
    ]),
      password: new FormControl('',[
        Validators.required
      ]),
      confirmPassword:new FormControl('',[
        Validators.required
      ]),
    })
   }

  ngOnInit() {
    
  }
  submit(){
    let model={
      userName:this.myForm.value.userName,
      password:this.myForm.value.password,
      email:this.myForm.value.email,
    }
    this.remote.post('http://localhost:49432/Account/Register',model).subscribe(res=>{
      if(res.IsSuccess){
        let data =res.Data as AuthResult;
        debugger;
      }else{
        
      }
    });
  }
}
