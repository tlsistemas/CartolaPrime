using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models.Push
{
    public class DeviceInstallation
    {
        [JsonProperty("installationId")]
        public string InstallationId { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("pushChannel")]
        public string PushChannel { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
