import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Cliente } from '../../core/models/clientes';

@Component({
  selector: 'cliente-form',
  templateUrl: './form.component.html',
  styleUrl: './form.component.css'
})
export class FormComponent {

  @Output() newUserEvent = new EventEmitter();
  @Input() cliente:Cliente = new Cliente()


  OnSubmit(): void {
    console.log('form',this.cliente);
    this.newUserEvent.emit(this.cliente);
    this.clean()
  }

  clean(): void{
    this.cliente = new Cliente()
  }
}
