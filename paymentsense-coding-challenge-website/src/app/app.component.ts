import { Component } from '@angular/core';
import { faThumbsUp, faThumbsDown } from '@fortawesome/free-regular-svg-icons';

import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

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

  public countryNames$: Observable<string[]>;

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

    this.countryNames$ = this.paymentsenseCodingChallengeApiService.getCountryNames();
  }
}
