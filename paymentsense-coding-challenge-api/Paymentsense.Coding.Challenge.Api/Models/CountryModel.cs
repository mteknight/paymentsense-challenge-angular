using System.Collections.Generic;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class CountryModel
    {
        public string Name { get; set; }

        public string Flag { get; set; }

        public uint Population { get; set; }

        public ICollection<string> Timezones { get; set; }

        public ICollection<CurrencyModel> Currencies { get; set; }

        public ICollection<LanguageModel> Languages { get; set; }

        public string Capital { get; set; }

        public ICollection<string> Borders { get; set; }
    }
}