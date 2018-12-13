import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MakesService {
  url = 'https://localhost:5001/api/vega/makes';

  constructor(private http: HttpClient) { }

  getMakes(): Observable<any> {
    return this.http.get(this.url).pipe();
  }
}
