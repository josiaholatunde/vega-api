import { Vehicle } from './../../Models/Vehicle';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from 'src/app/service/vehicle.service';
import { MakesService } from 'src/app/service/makes.service';
import Make from 'src/app/Models/Make';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  vehicles: Vehicle[];
  allVehicles: Vehicle[];
  makes: Make[];
  filter: any = {};
  constructor(private vehicleService: VehicleService, private makesService: MakesService) {
  }

  ngOnInit() {
    this.makesService.getMakes().subscribe(makes => this.makes = makes);
    this.populateVehicles();
  }
  populateVehicles() {
    this.vehicleService.getVehicles(this.filter).subscribe(vehicles => {
      this.vehicles = this.allVehicles = <Vehicle[]>vehicles;
    });
  }
  getVehicles() {
  }
  onFilterChangeClient() {
    let vehicles = this.allVehicles;
    if (this.filter.makeId) {
      vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
    }
    if (this.filter.modelId) {
      vehicles = vehicles.filter(v => v.model.id == this.filter.modelId);
    }
    this.vehicles = vehicles;
  }
  onFilterChange() {
    this.populateVehicles();
  }
  reset() {
    this.filter = {};
    this.onFilterChange();
  }
  sortColumns(columnName: string) {
    if (this.filter.sortBy === columnName) {
      this.filter.isAscending = false;
    } else {
      this.filter.isAscending = true;
      this.filter.sortBy = columnName;
    }
    this.populateVehicles();
  }

}
