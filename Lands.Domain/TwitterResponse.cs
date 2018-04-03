namespace Lands.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Url2
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonProperty(PropertyName = "display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty(PropertyName = "indices")]
        public List<int> Indices { get; set; }
    }

    public class Url
    {
        [JsonProperty(PropertyName = "urls")]
        public List<Url2> Urls { get; set; }
    }

    public class Description
    {
        [JsonProperty(PropertyName = "urls")]
        public List<object> Urls { get; set; }
    }

    public class Entities
    {
        [JsonProperty(PropertyName = "url")]
        public Url Url { get; set; }

        [JsonProperty(PropertyName = "description")]
        public Description Description { get; set; }
    }

    public class UserMention
    {
        [JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "id_str")]
        public string IdStr { get; set; }

        [JsonProperty(PropertyName = "indices")]
        public List<int> Indices { get; set; }
    }

    public class UserMention2
    {
        [JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "id_str")]
        public string IdStr { get; set; }

        [JsonProperty(PropertyName = "indices")]
        public List<int> Indices { get; set; }
    }

    public class Entities2
    {
        [JsonProperty(PropertyName = "hashtags")]
        public List<object> HashTags { get; set; }

        [JsonProperty(PropertyName = "symbols")]
        public List<object> Symbols { get; set; }

        [JsonProperty(PropertyName = "user_mentions")]
        public List<UserMention2> UserMentions { get; set; }

        [JsonProperty(PropertyName = "urls")]
        public List<object> Urls { get; set; }
    }

    public class Entities3
    {
        [JsonProperty(PropertyName = "hashtags")]
        public List<object> HashTags { get; set; }

        [JsonProperty(PropertyName = "symbols")]
        public List<object> Symbols { get; set; }

        [JsonProperty(PropertyName = "user_mentions")]
        public List<UserMention2> UserMentions { get; set; }

        [JsonProperty(PropertyName = "urls")]
        public List<object> Urls { get; set; }
    }

    public class RetweetedStatus
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "id_str")]
        public string IdStr { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "truncated")]
        public bool Truncated { get; set; }

        [JsonProperty(PropertyName = "entities")]
        public Entities3 Entities { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty(PropertyName = "geo")]
        public object Geo { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty(PropertyName = "place")]
        public object Place { get; set; }

        [JsonProperty(PropertyName = "contributors")]
        public object Contributors { get; set; }

        [JsonProperty(PropertyName = "is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty(PropertyName = "retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty(PropertyName = "favorite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty(PropertyName = "favorited")]
        public bool Favorited { get; set; }

        [JsonProperty(PropertyName = "retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty(PropertyName = "lang")]
        public string Lang { get; set; }
    }

    public class Status
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "id_str")]
        public string IdStr { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "truncated")]
        public bool Truncated { get; set; }

        [JsonProperty(PropertyName = "entities")]
        public Entities2 Entities { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_status_id")]
        public object InReplyToStatusId { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_user_id")]
        public object InReplyToUserId { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_user_id_str")]
        public object InReplyToUserIdStr { get; set; }

        [JsonProperty(PropertyName = "in_reply_to_screen_name")]
        public object InReplyToScreenName { get; set; }

        [JsonProperty(PropertyName = "geo")]
        public object Geo { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public object Coordinates { get; set; }

        [JsonProperty(PropertyName = "place")]
        public object Place { get; set; }

        [JsonProperty(PropertyName = "contributors")]
        public object Contributors { get; set; }

        [JsonProperty(PropertyName = "retweeted_status")]
        public RetweetedStatus RetweetedStatus { get; set; }

        [JsonProperty(PropertyName = "is_quote_status")]
        public bool IsQuoteStatus { get; set; }

        [JsonProperty(PropertyName = "retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty(PropertyName = "favorite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty(PropertyName = "favorited")]
        public bool Favorited { get; set; }
    }

    public class TwitterResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "id_str")]
        public string IdStr { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "entities")]
        public Entities Entities { get; set; }

        [JsonProperty(PropertyName = "followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty(PropertyName = "friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty(PropertyName = "listed_count")]
        public int ListedCount { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "favourites_count")]
        public int FavouritesCount { get; set; }

        [JsonProperty(PropertyName = "utc_offset")]
        public int? UtcOffset { get; set; }

        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty(PropertyName = "geo_enabled")]
        public bool GeoEnabled { get; set; }

        [JsonProperty(PropertyName = "verified")]
        public bool IsVerified { get; set; }

        [JsonProperty(PropertyName = "statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty(PropertyName = "lang")]
        public string Lang { get; set; }

        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }

        [JsonProperty(PropertyName = "contributors_enabled")]
        public bool ContributorsEnabled { get; set; }

        [JsonProperty(PropertyName = "is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty(PropertyName = "is_translation_enabled")]
        public bool IsTranslationEnabled { get; set; }

        [JsonProperty(PropertyName = "profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty(PropertyName = "profile_background_image_url")]
        public string ProfileBackgroundImageUrl { get; set; }

        [JsonProperty(PropertyName = "profile_background_image_url_https")]
        public string ProfileBackgroundImageUrlHttps { get; set; }

        [JsonProperty(PropertyName = "profile_background_tile")]
        public bool ProfileBackgroundTile { get; set; }

        [JsonProperty(PropertyName = "profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty(PropertyName = "profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        [JsonProperty(PropertyName = "profile_banner_url")]
        public string ProfileBannerUrl { get; set; }

        [JsonProperty(PropertyName = "profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty(PropertyName = "profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor { get; set; }

        [JsonProperty(PropertyName = "profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor { get; set; }

        [JsonProperty(PropertyName = "profile_text_color")]
        public string ProfileTextColor { get; set; }

        [JsonProperty(PropertyName = "profile_use_background_image")]
        public bool ProfileUseBackgroundImage { get; set; }

        [JsonProperty(PropertyName = "has_extended_profile")]
        public bool HasExtendedProfile { get; set; }

        [JsonProperty(PropertyName = "default_profile")]
        public bool DefaultProfile { get; set; }

        [JsonProperty(PropertyName = "default_profile_image")]
        public bool DefaultProfileImage { get; set; }

        [JsonProperty(PropertyName = "following")]
        public bool Following { get; set; }

        [JsonProperty(PropertyName = "follow_request_sent")]
        public bool FollowRequestSent { get; set; }

        [JsonProperty(PropertyName = "notifications")]
        public bool Notifications { get; set; }

        [JsonProperty(PropertyName = "translator_type")]
        public string TranslatorType { get; set; }
    }
}
