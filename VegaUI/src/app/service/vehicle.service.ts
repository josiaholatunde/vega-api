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
}
