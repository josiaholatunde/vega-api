import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FeaturesService {
  url = 'https://localhost:5001/api/vega/features';

  constructor(private http: HttpClient) { }

  getFeatures(): Observable<any> {
    return this.http.get(this.url).pipe();
  }
}
