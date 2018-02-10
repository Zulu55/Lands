namespace Lands.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class Land
    {
        [JsonProperty(PropertyName = "acronym")]
        public string name { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<string> topLevelDomain { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string alpha2Code { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string alpha3Code { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<string> callingCodes { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string capital { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<string> altSpellings { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string region { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string subregion { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public int population { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<int> latlng { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string demonym { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public int area { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public double gini { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<string> timezones { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<string> borders { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string nativeName { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string numericCode { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<Currency> currencies { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<Language> languages { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public Translations translations { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string flag { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public List<RegionalBloc> regionalBlocs { get; set; }

        [JsonProperty(PropertyName = "acronym")]
        public string cioc { get; set; }
    }
}