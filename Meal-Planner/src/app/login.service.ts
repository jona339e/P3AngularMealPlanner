import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, catchError, retry} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = 'https://localhost:7268/';

  constructor(private http:HttpClient) { }

  sendLoginData(username: string, password: string): Observable<any>{
    
    const body ={
      username: username,
      password: password
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post(`${this.apiUrl}validate`, body,{headers, responseType: 'json'});
  }

  createLogin(data: any): Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    })

    return this.http.post(`${this.apiUrl}api/User`, data,{headers, responseType: 'text'});
  }

  //set usertoken to storage
  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  //check for token
  isLoggedIn(): boolean{
    return !! localStorage.getItem('token');
  }
}
