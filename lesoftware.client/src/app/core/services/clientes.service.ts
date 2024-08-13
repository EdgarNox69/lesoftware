import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Cliente } from '../models/clientes';
import { url_base } from '../../shared/env';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {

  constructor(private http:HttpClient) { }

  lista(): Observable<Cliente[]>{
    return this.http
    .post<Cliente[]>(url_base+'Cliente/lista', '')
    .pipe(map(res =>{
      return res
    }))
  }

  update(cliente:Cliente): Observable<Cliente>{
    return this.http
    .post<Cliente>(url_base+'Cliente/editar', cliente)
    .pipe(map(res=>{
      return res
    }))
  }

  create(cliente:Cliente): Observable<Cliente>{
    return this.http
    .post<Cliente>(url_base+'Account/registrar', cliente)
    .pipe(map(res=>{
      return res
    }))
  }

  delete(cliente:Cliente): Observable<Cliente>{
    return this.http
    .post<Cliente>(url_base+'Cliente/eliminar', cliente)
    .pipe(map(res=>{
      return res
    }))
  }
}
