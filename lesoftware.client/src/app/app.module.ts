import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClientesComponent } from './clientes/clientes.component';
import { FormComponent } from './clientes/form/form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { TiendasComponent } from './tiendas/tiendas.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { ArticulosFormComponent } from './articulos/form/form.component';
import { CarritoComponent } from './carrito/carrito.component';
import { TiendaformComponent } from './tiendas/tiendaform/tiendaform.component';
import { CurriculumComponent } from './curriculum/curriculum.component';

@NgModule({
  declarations: [
    AppComponent,
    ClientesComponent,
    FormComponent,
    LoginComponent,
    LayoutComponent,
    TiendasComponent,
    ArticulosComponent,
    ArticulosFormComponent,
    CarritoComponent,
    TiendaformComponent,
    CurriculumComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, CommonModule, ReactiveFormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
