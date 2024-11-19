import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientesComponent } from './clientes/clientes.component';
import { LoginComponent } from './login/login.component';
import { LayoutComponent } from './shared/layout/layout.component';
import { TiendasComponent } from './tiendas/tiendas.component';
import { ArticulosComponent } from './articulos/articulos.component';
import { CarritoComponent } from './carrito/carrito.component';
import { isLoggedGuard } from './core/guards/is-logged.guard';
import { CurriculumComponent } from './curriculum/curriculum.component';

const routes: Routes = [
  {
    path:'',
    redirectTo:'/curriculum',
    pathMatch:'full'
  },
  {
    path:'layout',
    component:LayoutComponent,
    //canMatch: [isLoggedGuard],
    children:[
    ]
  },
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
  },
  {
    path:'tienda',
    component:TiendasComponent
  },
  {
    path:'curriculum',
    component:CurriculumComponent
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
