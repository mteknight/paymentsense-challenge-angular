import { Component } from '@angular/core';
import { faThumbsUp, faThumbsDown } from '@fortawesome/free-regular-svg-icons';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { PageEvent } from '@angular/material/paginator';

import { PaymentsenseCodingChallengeApiService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public faThumbsUp = faThumbsUp;
  public faThumbsDown = faThumbsDown;
  public title = 'Paymentsense Coding Challenge!';
  public paymentsenseCodingChallengeApiIsActive = false;
  public paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
  public paymentsenseCodingChallengeApiActiveIconColour = 'red';

  public countryNamesPage: string[];
  public pageSize: number = 10;
  public listLength: number = 1;
  public pageSizeOptions: number[] = [10, 25, 50, 100];

  constructor(private paymentsenseCodingChallengeApiService: PaymentsenseCodingChallengeApiService) {
    paymentsenseCodingChallengeApiService.getHealth().pipe(take(1))
      .subscribe(
        apiHealth => {
          this.paymentsenseCodingChallengeApiIsActive = apiHealth === 'Healthy';
          this.paymentsenseCodingChallengeApiActiveIcon = this.paymentsenseCodingChallengeApiIsActive
            ? this.faThumbsUp
            : this.faThumbsUp;
          this.paymentsenseCodingChallengeApiActiveIconColour = this.paymentsenseCodingChallengeApiIsActive
            ? 'green'
            : 'red';
        },
        _ => {
          this.paymentsenseCodingChallengeApiIsActive = false;
          this.paymentsenseCodingChallengeApiActiveIcon = this.faThumbsDown;
          this.paymentsenseCodingChallengeApiActiveIconColour = 'red';
        });

        this.getCountryNames();
  }

  public getCountryNames(event?: PageEvent): PageEvent {

    let currentPage: number = 1;
    if (event) {

      this.pageSize = event.pageSize;
      currentPage = event.pageIndex + 1;
    }

    this.paymentsenseCodingChallengeApiService.getCountryNames()
      .subscribe((
        countryNames: string[]) => {

        const pageStart = (currentPage - 1) * this.pageSize;
        const pageEnd = currentPage * this.pageSize;

        this.listLength = countryNames.length;
        this.countryNamesPage = countryNames.slice(pageStart, pageEnd);
        },
        error => {

          this.countryNamesPage = [error];
        });

      return event;
  }
}
