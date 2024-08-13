import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Articulos } from '../../core/models/articulos';

@Component({
  selector: 'articulos-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class ArticulosFormComponent {
  @Output() newArticuloEvent = new EventEmitter();
  @Input() articulo:Articulos = new Articulos();

  OnSubmit(){
    this.newArticuloEvent.emit(this.articulo);
    this.clean()
  }

  clean(): void{
    this.articulo = new Articulos()
  }
}
