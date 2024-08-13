import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Articulos } from '../models/articulos';
import { url_base } from '../../shared/env';
import { Carrito } from '../models/carrito';

@Injectable({
  providedIn: 'root'
})
export class ArticulosService {

  constructor(private http:HttpClient) { }

  lista(): Observable<Articulos[]>{
    return this.http
    .post<Articulos[]>(url_base+'Articulo/lista', '')
    .pipe(map(res =>{
      return res
    }))
  }

  update(articulo:Articulos): Observable<Articulos>{
    return this.http
    .post<Articulos>(url_base+'Articulo/editar', articulo)
    .pipe(map(res=>{
      return res
    }))
  }

  create(articulo:Articulos): Observable<Articulos>{
    return this.http
    .post<Articulos>(url_base+'Articulo/agregar', articulo)
    .pipe(map(res=>{
      return res
    }))
  }

  delete(articulo:Articulos): Observable<Articulos>{
    return this.http
    .post<Articulos>(url_base+'Articulo/eliminar', articulo)
    .pipe(map(res=>{
      return res
    }))
  }

  carrito(carrito:Carrito){
    console.log(carrito)
    return this.http
    .post<Carrito>(url_base+'Carrito/carrito', carrito)
    .pipe(map(res=>{
      return res
    }))
  }

}
