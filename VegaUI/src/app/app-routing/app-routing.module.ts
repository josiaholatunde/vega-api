import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { VehicleFormComponent } from '../component/vehicle-form/vehicle-form.component';
import { NotFoundComponent } from '../component/not-found/not-found.component';
import { VehicleListComponent } from '../component/vehicle-list/vehicle-list.component';

const routes: Routes = [
  { path: 'vehicle/new', component: VehicleFormComponent},
  { path: 'vehicle/:id', component: VehicleFormComponent},
  { path: 'vehicles', component: VehicleListComponent},
  { path: '**', component: NotFoundComponent },
  { path: '', redirectTo: 'vehicles', pathMatch: 'full'}
];
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
