import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './clientes/clientes.component';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { TiendasComponent } from './tiendas/tiendas.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { CarritoComponent } from './carrito/carrito.component';

const routes: Routes = [
  {
    path:'',
    redirectTo:'login',
    pathMatch:'full'
  },
  {
    path:'layout',
    component:LayoutComponent,
    children:[
      {
        path:'clientes',
        component:ClientesComponent
      },
      {
        path:'tiendas',
        component:TiendasComponent
      },
      {
        path:'articulos',
        component:ArticulosComponent
      },
      {
        path:'carrito',
        component:CarritoComponent
      }
    ]
  },
  {
    path:'login',
    component:LoginComponent
  },
  {
    path:'clientes',
    component:ClientesComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
