import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.css']
})
export class VehicleDetailComponent implements OnInit {

  @Input() vehiclesList;
  constructor() { }

  ngOnInit() {
  }

}
