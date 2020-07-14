import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { Autofixture } from 'ts-autofixture/dist/src/index';

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

  public getCountryNames(): Observable<string[]> {

    let countryNames: string[] = [];
    while (countryNames.push(Autofixture.createString()) < 12);

    return of(countryNames);
  }
}
