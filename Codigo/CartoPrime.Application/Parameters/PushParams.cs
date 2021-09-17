using CartoPrime.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TM.Utils;
using TM.Utils.Bases;
using TM.Utils.Delegates;

namespace CartoPrime.Application.Parameters
{
    public class PushParams : BaseParams<PushNotification>
    {
        public int UserId { get; set; }
        public bool Android { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceId { get; set; }
        public DateTime SendDate { get; set; }
        public string CodeType { get; set; }
        public string Type { get; set; }

        public override Expression<Func<PushNotification, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<PushNotification>();

            if (!string.IsNullOrWhiteSpace(Key))
            {
                var user = new PushNotification { Key = Key.UrlDecode() };
                predicate = predicate.And(p => p.Id.Equals(user.Id));
            }

            if (Id.HasValue)
            {
                predicate = predicate.And(p => p.Id == Id);
            }

            if (UserId > 0)
            {
                predicate = predicate.And(p => p.UserId == UserId);
            }
            if (DeviceVersion != null)
            {
                predicate = predicate.And(p => p.DeviceVersion == DeviceVersion);
            }

            if (Android)
            {
                predicate = predicate.And(p => p.Android.Equals(Android));
            }

            if (DeviceId != null)
            {
                predicate = predicate.And(p => p.DeviceId.Equals(DeviceId));
            }

            //if (SendDate.Date > DateTime.MinValue)
            //{
            //    predicate = predicate.And(p => p.SendDate.Equals(SendDate));
            //}

            //if (!string.IsNullOrWhiteSpace(CodeType))
            //{
            //    predicate = predicate.And(p => p.CodeType.Equals(CodeType));
            //}

            //if (!string.IsNullOrWhiteSpace(Type))
            //{
            //    predicate = predicate.And(p => p.Type.Equals(Type));
            //}


            return predicate;
        }
    }
}
