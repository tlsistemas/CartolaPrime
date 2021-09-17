using System;
using System.Collections.Generic;
using System.Text;
using TM.Utils.Queue;

namespace CartolaPrime.SendPush.Queue
{
    public class QueuePush : QueueBase<QueuePush>, IQueueBase
    {
        String IQueueBase.QueueName() { return @".\private$\via"; }
        TimeSpan IQueueBase.TimeLife() { return TimeSpan.MaxValue; }

        public String Type { get; set; }
        public String Json { get; set; }
    }
}
