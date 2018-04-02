namespace Lands.Domain
{
    using Newtonsoft.Json;

    public class Counts
    {
        [JsonProperty("media")]
        public int Media { get; set; }

        [JsonProperty("follows")]
        public int Follows { get; set; }

        [JsonProperty("followed_by")]
        public int FollowedBy { get; set; }
    }

    public class UserData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("is_business")]
        public bool IsBusiness { get; set; }

        [JsonProperty("counts")]
        public Counts Counts { get; set; }
    }

    public class Meta
    {
        [JsonProperty("code")]
        public int Code { get; set; }
    }

    public class InstagramResponse
    {
        [JsonProperty("data")]
        public UserData UserData { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}