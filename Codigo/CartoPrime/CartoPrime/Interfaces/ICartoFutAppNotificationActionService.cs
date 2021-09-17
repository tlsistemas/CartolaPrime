using CartoPrime.Models.Enum;
using System;

namespace CartoPrime.Interfaces
{
    public interface ICartoPrimeAppNotificationActionService : INotificationActionService
    {
        event EventHandler<PushAction> ActionTriggered;
    }
}
