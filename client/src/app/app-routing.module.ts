import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EditOrderComponent} from "./edit-order/edit-order.component";
import {OrderListComponent} from "./order-list/order-list.component";

const routes: Routes = [
  { path: '', redirectTo: '/order-list', pathMatch: 'full' },
  { path:  'edit-order', component:  EditOrderComponent},
  { path:  'order-list', component:  OrderListComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
