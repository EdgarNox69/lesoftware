import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/clientes';
import { url_base } from '../../shared/env';

@Injectable({
  providedIn: 'root'
})
export class LayoutService {

  constructor(private http:HttpClient) { }

  cliente(clienteId:number): Observable<Cliente>{
    return this.http.post<Cliente>(url_base+'Cliente/cliente', clienteId)
  }
}
