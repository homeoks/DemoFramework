using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Infrastructure;
using Newtonsoft.Json;

namespace Service.ViewModel
{
    public class TokenAuthRequest
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username is not correct format")]
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("sex")]
        public SexType SexType { get; set; }
    }
    public class TokenAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
