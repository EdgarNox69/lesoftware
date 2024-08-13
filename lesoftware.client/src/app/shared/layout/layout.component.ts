import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../../core/services/layout.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent implements OnInit{

  constructor(private service:LayoutService){}
  cliente:string = ''
  ngOnInit(): void {
    const storedData:any = localStorage.getItem('user')
    const clienteId = JSON.parse(storedData)
    console.log(clienteId)
    this.service.cliente(clienteId.id).subscribe({
      next:(res)=>{
        this.cliente = res.nombre
      }
    })
  }
}
