import { Component, OnInit, ViewChild } from '@angular/core';
import { ClientesService } from '../core/services/clientes.service';
import { Cliente } from '../core/models/clientes';
import { FormComponent } from './form/form.component';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.css'
})
export class ClientesComponent implements OnInit{

  @ViewChild(FormComponent) form!: FormComponent

  constructor(private service:ClientesService){}
  clientes: Cliente[] = []
  clienteSelected: Cliente = new Cliente()
  ngOnInit(): void {

      this.service.lista().subscribe(clientes => this.clientes = clientes)
      console.log(this.clientes)
  }

  onEditar(clienteRow:Cliente){
    this.clienteSelected = {... clienteRow}
  }

  onDelete(clienteRow:Cliente){
    this.service.delete(clienteRow).subscribe({
      next:(res)=>{
        alert('El cliente se eliminó correctamente')
        this.clientes = this.clientes.filter(c => c.id != clienteRow.id)
      },
      error:(err)=>{
        alert('Hubo un error al eliminar el cliente')
      }
    })
  }

  addcliente(clienteRow:Cliente){
      if(clienteRow.id > 0){
        this.service.update(clienteRow).subscribe({
          next:(clienteact)=>{
            console.log(clienteact)
            this.clientes = this.clientes.map( cliente => {
              if(cliente.id == clienteRow.id){
                return{ ... clienteact}
              }
              return cliente
            })
            alert("El usuario se actualizó correctamente")
          },
          error: (err)=>{
            alert('Hubo un problema al actualizar los datos del cliente')
          }
        })
      }else{
        this.service.create(clienteRow).subscribe({
          next:(ClienteNew)=>{
            this.clientes = [... this.clientes, {... ClienteNew}]
            alert('El cliente se agregó correctamente')
          },
          error:(err)=>{
            alert('Hubo un problema al agregar el cliente')
          }
        })
      }
  }

}
