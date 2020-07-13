import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MockPaymentsenseCodingChallengeApiService {
  public getHealth(): Observable<string> {
    return of('Healthy');
  }

  public getCountryNames(): Observable<string[]> {

    const countryNames = ['Country1', 'Country2', 'Country3'];
    return of(countryNames);
  }
}
