using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Bases;

namespace CartoPrime.Domain.Entities
{
    public class PushNotification : EntityBase
    {
        public int UserId { get; set; }
        public bool Android { get; set; }
        public string DeviceVersion { get; set; }
        public string FCMToken { get; set; }
        public string DeviceId { get; set; }
        public string DeviceBuild { get; set; }
    }
}
