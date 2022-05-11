import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PromotionDto } from 'src/models/promotion.dto';

@Injectable({
  providedIn: 'root'
})
export class PackageService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get('https://localhost:7274/api/package');
  }
}
