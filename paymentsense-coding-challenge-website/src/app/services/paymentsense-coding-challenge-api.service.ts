import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Country } from '../models/country';

@Injectable({
  providedIn: 'root'
})
export class PaymentsenseCodingChallengeApiService {
  constructor(private httpClient: HttpClient) { }

  public getHealth(): Observable<string> {
    return this.httpClient.get('https://localhost:5001/health', { responseType: 'text' });
  }

  public getCountries(): Observable<Country[]> {

    return this.httpClient.get<Country[]>('https://localhost:5001/countries');
  }
}
