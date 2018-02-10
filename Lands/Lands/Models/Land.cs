namespace Lands.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Land
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "topLevelDomain")]
        public List<string> TopLevelDomain { get; set; }

        [JsonProperty(PropertyName = "alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonProperty(PropertyName = "alpha3Code")]
        public string Alpha3Code { get; set; }

        [JsonProperty(PropertyName = "callingCodes")]
        public List<string> CallingCodes { get; set; }

        [JsonProperty(PropertyName = "capital")]
        public string Capital { get; set; }

        [JsonProperty(PropertyName = "altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "subregion")]
        public string Subregion { get; set; }

        [JsonProperty(PropertyName = "population")]
        public int Population { get; set; }

        [JsonProperty(PropertyName = "latlng")]
        public List<double> Latlng { get; set; }

        [JsonProperty(PropertyName = "demonym")]
        public string Demonym { get; set; }

        [JsonProperty(PropertyName = "area")]
        public double? Area { get; set; }

        [JsonProperty(PropertyName = "gini")]
        public double? Gini { get; set; }

        [JsonProperty(PropertyName = "timezones")]
        public List<string> Timezones { get; set; }

        [JsonProperty(PropertyName = "borders")]
        public List<string> Borders { get; set; }

        [JsonProperty(PropertyName = "nativeName")]
        public string NativeName { get; set; }

        [JsonProperty(PropertyName = "numericCode")]
        public string NumericCode { get; set; }

        [JsonProperty(PropertyName = "currencies")]
        public List<Currency> Currencies { get; set; }

        [JsonProperty(PropertyName = "languages")]
        public List<Language> Languages { get; set; }

        [JsonProperty(PropertyName = "translations")]
        public Translations Translations { get; set; }

        [JsonProperty(PropertyName = "flag")]
        public string Flag { get; set; }

        [JsonProperty(PropertyName = "regionalBlocs")]
        public List<RegionalBloc> RegionalBlocs { get; set; }

        [JsonProperty(PropertyName = "cioc")]
        public string Cioc { get; set; }
    }
}