import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { LoginRequest } from '../models/login';
import { UserAuth } from '../models/clientAuth';
import { url_base } from '../../shared/env';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http:HttpClient) {
    this.currentUserLoginOn= new BehaviorSubject<boolean>(sessionStorage.getItem("token")!=null)
    this.currentUserData = new BehaviorSubject<String>(sessionStorage.getItem("token") || "")
 
   }

  currentUserLoginOn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false)
  currentUserData: BehaviorSubject<String> = new BehaviorSubject<String>("")
  credential: BehaviorSubject<UserAuth> = new BehaviorSubject<UserAuth>({id: 0, token: ''})

  Authentication(credentials:LoginRequest):Observable<any>{
    return this.http
    .post<any>(url_base+'Account/login', credentials)
    .pipe(
      tap((userData)=>{
        sessionStorage.setItem("token", userData.token)
        this.currentUserData.next(userData.token)
        this.currentUserLoginOn.next(true)
        this.credential.next(userData)
      }),
      map((userData)=>userData.token)
    )
  }


  logout():void{
    sessionStorage.removeItem("token")
    localStorage.removeItem("user")
  }

  get userData():Observable<String>{
    return this.currentUserData.asObservable();
  }

  get userLoginOn(): Observable<boolean>{
    return this.currentUserLoginOn.asObservable();
  }
}
