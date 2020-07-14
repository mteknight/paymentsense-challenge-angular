import { TestBed, async } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { PaymentsenseCodingChallengeApiService } from './services';
import { MockPaymentsenseCodingChallengeApiService } from './testing/mock-paymentsense-coding-challenge-api.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        FontAwesomeModule,
        BrowserAnimationsModule,
        MaterialModule
      ],
      declarations: [
        AppComponent
      ],
      providers: [
        { provide: PaymentsenseCodingChallengeApiService, useClass: MockPaymentsenseCodingChallengeApiService }
      ]
    }).compileComponents();
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Paymentsense Coding Challenge'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Paymentsense Coding Challenge!');
  });

  it('should render title in a h1 tag', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;

    expect(compiled.querySelector('h1').textContent).toContain('Paymentsense Coding Challenge!');
  });

  it('should get list of country names', () => {

    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    let countryNames = fixture.componentInstance.countriesPage

    expect(countryNames.length).toBe(10);
  });

  it('should render country names in li', () => {

    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    let listElements = compiled.querySelectorAll("ul[id='countries'] li");

    expect(listElements.length).toBe(10);
  })

  it('should render country flags in li', () => {

    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    let listElementImages = compiled.querySelectorAll("ul[id='countries'] li img");

    expect(listElementImages.length).toBe(10);
  })

  it('should render details for selected country', () => {

    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.debugElement.nativeElement;
    let selectedCountry = fixture.componentInstance.selectedCountry = fixture.componentInstance.countriesPage[1];
    fixture.detectChanges();
    let countryDetailsPanel = compiled.querySelector("div[id='countryDetails']");

    expect(selectedCountry).not.toBeNull();
    expect(countryDetailsPanel).not.toBeNull();
  })
});
