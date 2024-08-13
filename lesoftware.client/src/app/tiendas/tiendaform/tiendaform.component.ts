import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Tienda } from '../../core/models/tiendas';

@Component({
  selector: 'app-tiendaform',
  templateUrl: './tiendaform.component.html',
  styleUrl: './tiendaform.component.css'
})
export class TiendaformComponent {
  @Output() newTiendaEvent = new EventEmitter();
  @Input() tienda:Tienda = new Tienda()


  OnSubmit(): void {
    console.log('form',this.tienda);
    this.newTiendaEvent.emit(this.tienda);
    this.clean()
  }

  clean(): void{
    this.tienda = new Tienda()
  }
}
