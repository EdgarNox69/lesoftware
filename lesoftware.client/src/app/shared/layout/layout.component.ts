import { Component, OnInit } from '@angular/core';
import { LayoutService } from '../../core/services/layout.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent implements OnInit{

  constructor(private service:LayoutService, private router:Router){}
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

  logout(){
    this.service.logout()
    this.router.navigate(['/'])
  }
}
