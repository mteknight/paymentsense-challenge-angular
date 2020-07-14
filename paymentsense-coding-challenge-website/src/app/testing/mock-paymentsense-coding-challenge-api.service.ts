import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Autofixture } from 'ts-autofixture/dist/src/index';

import { Country } from '../models/country';

@Injectable({
  providedIn: 'root'
})
export class MockPaymentsenseCodingChallengeApiService {

  private fixture: Autofixture;

  constructor() {

    this.fixture = new Autofixture();
  }

  public getHealth(): Observable<string> {
    return of('Healthy');
  }

  public getCountries(): Observable<Country[]> {

    let country: Country;
    const countries = this.fixture.createMany(country, 12);

    return of(countries);
  }
}
