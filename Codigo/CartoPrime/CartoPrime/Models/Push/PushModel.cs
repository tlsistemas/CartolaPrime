using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Models.Push
{
    public class PushModel
    {
        public int UserId { get; set; }
        public bool Android { get; set; }
        public string DeviceVersion { get; set; }
        public string FCMToken { get; set; }
        public string DeviceId { get; set; }
        public string DeviceBuild { get; set; }
        public DateTime SendDate { get; set; }
    }
}
