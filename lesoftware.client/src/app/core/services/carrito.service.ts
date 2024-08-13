import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Articulos } from '../models/articulos';
import { url_base } from '../../shared/env';
import { ArticulosCarrito } from '../models/articulosCarrito';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {

  constructor(private http:HttpClient) { }

  lista(clienteId:number): Observable<ArticulosCarrito[]>{
    console.log(clienteId)
    return this.http
    .post<ArticulosCarrito[]>(url_base+'Carrito/lista', clienteId)
    .pipe(map(res=>{
      return res
    }))
  }

}
