import { Currency } from './currency';
import { Language } from './language';

export class Country {

  public name: string;
  public flag: string;
  public population: number;
  public timezones: string[];
  public currencies: Currency[];
  public languages: Language[];
  public capital: string;
  public borders:string[];
}
