import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { LoginService } from '../core/services/login.service';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginRequest } from '../core/models/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

 
  
  constructor(private formBuilder: FormBuilder, private router:Router, private loginService: LoginService){
    
   }

  ngOnInit(): void {
    
  }

  loginForm = this.formBuilder.group({

    nombre:['', [Validators.required]],
    password:['', [Validators.required]],
  })

  get nombre(){

    return this.loginForm.controls.nombre
  }

  get password(){

    return this.loginForm.controls.password
  }

  login(){
    if(this.loginForm.valid){
      this.loginService.Authentication(this.loginForm.value as LoginRequest).subscribe({
        next:(userData)=>{
          this.loginService.credential.subscribe(datos =>{
            console.log('Se hizo el get',datos)
            localStorage.setItem('user', JSON.stringify(datos))
          })
          this.router.navigateByUrl('/clientes')
          this.loginForm.reset()
        }
      })
    }
  }

  
}
