import * as _ from 'underscore';
import { Vehicle } from '../../Models/Vehicle';
import { Component, OnInit } from '@angular/core';
import { MakesService } from 'src/app/service/makes.service';
import { FeaturesService } from 'src/app/service/feature.service';
import { VehicleService } from 'src/app/service/vehicle.service';
import { SaveVehicle } from 'src/app/Models/SaveVehicle';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[] = [];
  models: any[] = [];
  features: any[] = [];
  vehicle: SaveVehicle;
  constructor(private route: ActivatedRoute, private router: Router, private makesService: MakesService,
    private featuresService: FeaturesService, private vehicleService: VehicleService) {
    this.vehicle = {
      id: 0,
      makeId: 0,
      modelId: 0,
      isRegistered: false,
      vehicleFeatures: [],
      contact: {
        name: '',
        phone: 0,
        email: ''
      }
    };
    route.params.subscribe( param => this.vehicle.id = +param['id'] || 0);
  }

  ngOnInit() {
    this.makesService.getMakes().subscribe(makes => this.makes = makes);
    this.featuresService.getFeatures().subscribe(features => this.features = features);
    if (this.vehicle.id) {
      this.vehicleService.getVehicle(this.vehicle.id).subscribe(
        (vehicle: Vehicle) => {
          this.setVehicle(vehicle);
          this.populateModels();
        },
        err => {
          if (err.status === 404) {
            this.router.navigate(['not-found']);
          }
        });
    }
  }
  private setVehicle(vehicle: Vehicle) {
    this.vehicle.id = vehicle.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.contact = vehicle.contact;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.vehicleFeatures = _.pluck(vehicle.vehicleFeatures, 'id');
  }
  updateModel() {
    this.populateModels();
    delete this.vehicle.modelId;
  }
  populateModels() {
    const selectedMake = this.makes.find(m => +m.id === +this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  updateFeatures() {
    const selectedModel = this.models.find(model => +model.id === this.vehicle.modelId);
  }
  onFeatureChange(featureId: number, $event: any ) {
    if ($event.target.checked) {
      this.vehicle.vehicleFeatures.push(featureId);
    } else {
      const index = this.vehicle.vehicleFeatures.indexOf(featureId);
      this.vehicle.vehicleFeatures.splice(index, 1);
    }
  }
  submitForm() {
    if (this.vehicle.id) {
      this.vehicleService.updateVehicle(this.vehicle).subscribe(vehicle => this.vehicle = <SaveVehicle>vehicle);
    } else {
      this.vehicleService.createVehicle(this.vehicle).subscribe( vehicle => console.log(vehicle));
    }
  }
  deleteVehicle() {
    if (confirm('Are you sure you want to delete this vehicle? ')) {
      this.vehicleService.deleteVehicle(this.vehicle.id).subscribe(id => {
        this.router.navigate(['vehicles']);
      });
    }
  }

}
