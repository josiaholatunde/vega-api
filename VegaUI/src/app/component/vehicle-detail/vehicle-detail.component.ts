import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-vehicle-detail',
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.css']
})
export class VehicleDetailComponent implements OnInit {

  @Input() vehiclesList;
  @Output() sortChanged = new EventEmitter();
  constructor() { }

  ngOnInit() {
  }
  sortBy(columnName: string) {
    this.sortChanged.emit(columnName);
  }

}
