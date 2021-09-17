using CartoPrime.Helpers;
using CartoPrime.Interfaces;
using CartoPrime.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Container
{
    public static class Bootstrap
    {
        public static void Begin(Func<IDeviceInstallationService> deviceInstallationService)
        {
            ServiceContainer.Register(deviceInstallationService);
            ServiceContainer.Register<ICartoPrimeAppNotificationActionService>(() => new NotificationActionService());
            ServiceContainer.Register<INotificationRegistrationService>(() => new NotificationRegistrationService(UrlService._registerPush, UrlService.ApiKey));

        }
    }
}
