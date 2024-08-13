import { Component, OnInit } from '@angular/core';
import { CarritoService } from '../core/services/carrito.service';
import { Articulos } from '../core/models/articulos';
import { ArticulosCarrito } from '../core/models/articulosCarrito';

@Component({
  selector: 'app-carrito',
  templateUrl: './carrito.component.html',
  styleUrl: './carrito.component.css'
})
export class CarritoComponent implements OnInit{

  constructor(private service:CarritoService){}
  carrito:ArticulosCarrito[] = []

  ngOnInit(): void {
   const storedData:any = localStorage.getItem('user')
   const clienteId = JSON.parse(storedData)
    console.log(clienteId)
      this.service.lista(clienteId.id).subscribe(carrito => this.carrito = carrito)
      console.log(this.carrito)
  }
}
