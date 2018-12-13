import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { VehicleFormComponent } from './component/vehicle-form/vehicle-form.component';
import { NavbarComponent } from './component/navbar/navbar.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { VehicleListComponent } from './component/vehicle-list/vehicle-list.component';
import { ComponentComponent } from './component/component.component';
import { NotFoundComponent } from './component/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    VehicleFormComponent,
    NavbarComponent,
    VehicleListComponent,
    ComponentComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
