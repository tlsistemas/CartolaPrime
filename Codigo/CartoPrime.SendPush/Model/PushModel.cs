using System;
using System.Collections.Generic;
using System.Text;

namespace CartolaPrime.SendPush.Model
{
    public class PushModel
    {
        public int UserId { get; set; }
        public bool Android { get; set; }
        public int Push { get; set; }
        public string FCMToken { get; set; }
        public Guid DeviceId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public string Icon { get; set; }
        public string Image { get; set; }
        public string CodeType { get; set; }
        public DateTime SendDate { get; set; }
    }
}
