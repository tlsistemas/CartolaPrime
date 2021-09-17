using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Application.ViewModel.User.Response
{
    public class UserToken
    {
        [JsonProperty("access_token", Order = 1)]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in", Order = 2)]
        public long ExpiresIn { get; set; }

        [JsonProperty("token_type", Order = 3)]
        public string TokenType { get; set; }

        [JsonProperty(".issued", Order = 4)]
        public DateTime Issued { get; set; }

        [JsonProperty(".expires", Order = 5)]
        public DateTime Expires { get; set; }

        [JsonProperty(".id", Order = 6)]
        public long Id { get; set; }

        [JsonProperty(".documentNumber", Order = 7)]
        public string DocumentNubmer { get; set; }

        [JsonProperty(".key", Order = 8)]
        public string Key { get; set; }
        [JsonProperty(".TypeUser", Order = 9)]
        public string TypeUser { get; set; }

        [JsonProperty(".name", Order = 10)]
        public string UserName { get; set; }
    }
}
