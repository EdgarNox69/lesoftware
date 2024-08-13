import { Component, OnInit } from '@angular/core';
import { TiendaService } from '../core/services/tienda.service';
import { Tienda } from '../core/models/tiendas';

@Component({
  selector: 'app-tiendas',
  templateUrl: './tiendas.component.html',
  styleUrl: './tiendas.component.css'
})
export class TiendasComponent implements OnInit{

  constructor(private service:TiendaService){}

  tiendas:Tienda[] = []
  tiendaSelected:Tienda = new Tienda()

  ngOnInit(): void {
    this.service.lista().subscribe(tienda => this.tiendas = tienda)
    console.log(this.tiendas)
  }

  onEditar(tiendaRow:Tienda){
    this.tiendaSelected = {... tiendaRow}
  }

  onDelete(tiendaRow:Tienda){
    this.service.delete(tiendaRow).subscribe({
      next:(res)=>{
        alert('La Tienda se eliminó correctamente')
        this.tiendas = this.tiendas.filter(c => c.id != tiendaRow.id)
      },
      error:(err)=>{
        alert('Hubo un error al eliminar la tienda')
      }
    })
  }

  addTienda(tiendaRow:Tienda){
    if(tiendaRow.id > 0){
      this.service.update(tiendaRow).subscribe({
        next:(tiendaact)=>{
          console.log(tiendaact)
          this.tiendas = this.tiendas.map( tienda => {
            if(tienda.id == tiendaRow.id){
              return{ ... tiendaact}
            }
            return tienda
          })
          alert("El usuario se actualizó correctamente")
        },
        error: (err)=>{
          alert('Hubo un problema al actualizar los datos del cliente')
        }
      })
    }else{
      this.service.create(tiendaRow).subscribe({
        next:(TiendaNew)=>{
          this.tiendas = [... this.tiendas, {... TiendaNew}]
          alert('El cliente se agregó correctamente')
        },
        error:(err)=>{
          alert('Hubo un problema al agregar el cliente')
        }
      })
    }
  }


}
