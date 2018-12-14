import { SaveVehicle } from 'src/app/Models/SaveVehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Vehicle } from 'Models/Vehicle';


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
  getVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(this.url).pipe();
  }
}
