import { SaveVehicle } from 'src/app/Models/SaveVehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from 'Models/Vehicle';
import { FilterResource } from '../Models/FilterResource';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  url = 'https://localhost:5001/api/vega/vehicles';

  constructor(private http: HttpClient) { }
  createVehicle(vehicle: any): Observable<any> {
    return this.http.post(this.url, vehicle, httpOptions ).pipe();
  }
  getVehicle(id: number): Observable<Vehicle> {
    return this.http.get(`${this.url}/${id}`).pipe();
  }
  updateVehicle(vehicle: SaveVehicle) {
    return this.http.put(`${this.url}/${vehicle.id}`, vehicle, httpOptions).pipe();
  }
  deleteVehicle(id: number) {
    return this.http.delete(`${this.url}/${id}`).pipe();
  }
  getVehicles(filter: FilterResource): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(`${this.url}?${this.toQueryString(filter)}`).pipe();
  }
  toQueryString(filter) {
    const params = [];
    filter.forEach(f => {
      const value = filter[f];
      if (value ) {
        const res = `${encodeURIComponent(filter)}=${encodeURIComponent(value)}`;
        params.push(res);
      }
    });
    console.log(params.join('&'));

  }
}
