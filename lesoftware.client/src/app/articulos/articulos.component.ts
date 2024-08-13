import { Component, OnInit } from '@angular/core';
import { ArticulosService } from '../core/services/articulos.service';
import { Articulos } from '../core/models/articulos';
import { JsonPipe } from '@angular/common';
import { Carrito } from '../core/models/carrito';

@Component({
  selector: 'app-articulos',
  templateUrl: './articulos.component.html',
  styleUrl: './articulos.component.css'
})
export class ArticulosComponent implements OnInit{

  constructor(private service:ArticulosService){}
  articulos:Articulos[] = []
  articuloSelected:Articulos = new Articulos()
  carrito:Carrito = new Carrito()
  ngOnInit(): void {
      this.service.lista().subscribe(articulos => this.articulos = articulos)
  }

  onEditar(articuloRow:Articulos){
    this.articuloSelected = {... articuloRow}
  }

  onDelete(articuloRow:Articulos){
    this.service.delete(articuloRow).subscribe({
      next:(res)=>{
        alert('El articulo se eliminó correctamente')
        this.articulos = this.articulos.filter(c => c.id != articuloRow.id)
      },
      error:(err)=>{
        alert('Hubo un error al eliminar el articulo')
      }
    })
  }

  OnAddCarrito(articuloRow:Articulos){
   const storedData:any = localStorage.getItem('user')
   const clienteId = JSON.parse(storedData)
    console.log(clienteId)
   this.carrito.clienteId = clienteId.id
   this.carrito.articuloId = articuloRow.id

    this.service.carrito(this.carrito).subscribe({
      next:(res)=>{
        alert('El articulo se agregó al carrito')
      },
      error:(err)=>{
        alert('Hubo un problema al agregar el articulo al carrito')
      }
    })
  }

  addarticulo(articuloRow:Articulos){
    if(articuloRow.id > 0){
      this.service.update(articuloRow).subscribe({
        next:(articuloactualizado)=>{
          console.log(articuloactualizado)
          this.articulos = this.articulos.map( articulo => {
            if(articulo.id == articuloRow.id){
              return{ ... articuloactualizado}
            }
            return articulo
          })
          alert("El articulo se actualizó correctamente")
        },
        error:(err)=>{
          alert("Hubo un error al actualizar el articulo")
        }
      })
    }else{
      this.service.create(articuloRow).subscribe({
        next:(articulonuevo)=>{
          this.articulos = [... this.articulos, {... articulonuevo}]
          alert('El articulo se agregó correctamente')
        },
        error:(err)=>{
          alert('Hubo un problema al agregar el articulo')
        }
      })
    }
  }
}
