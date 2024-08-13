import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Cliente } from '../models/clientes';
import { url_base } from '../../shared/env';
import { Tienda } from '../models/tiendas';

@Injectable({
  providedIn: 'root'
})
export class TiendaService {

  constructor(private http:HttpClient) { }

  lista(): Observable<Tienda[]>{
    return this.http
    .post<Tienda[]>(url_base+'Tienda/lista', '')
    .pipe(map(res =>{
      return res
    }))
  }

  update(tienda:Tienda): Observable<Tienda>{
    return this.http
    .post<Tienda>(url_base+'Tienda/editar', tienda)
    .pipe(map(res=>{
      return res
    }))
  }

  create(tienda:Tienda): Observable<Tienda>{
    return this.http
    .post<Tienda>(url_base+'Tienda/agregar', tienda)
    .pipe(map(res=>{
      return res
    }))
  }

  delete(tienda:Tienda): Observable<Tienda>{
    return this.http
    .post<Tienda>(url_base+'Tienda/eliminar', tienda)
    .pipe(map(res=>{
      return res
    }))
  }
}