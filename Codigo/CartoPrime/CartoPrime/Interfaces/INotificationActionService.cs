using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Interfaces
{
    public interface INotificationActionService
    {
        void TriggerAction(string action);
    }
}
