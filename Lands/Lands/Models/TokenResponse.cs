namespace Lands.Models
{
    using System;
    using Newtonsoft.Json;
    using SQLite.Net.Attributes;

    public class TokenResponse
    {
        #region Properties
        [PrimaryKey, AutoIncrement]
        public int TokenResponseId { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public string ErrorDescription { get; set; }
		#endregion

		#region Methods
		public override int GetHashCode()
		{
            return TokenResponseId;
		}
		#endregion
	}
}
