using CartoPrime.Models.Push;

namespace CartoPrime.Interfaces
{
    public interface IDeviceInstallationService
    {
        string Token { get; set; }
        bool NotificationsSupported { get; }
        string GetDeviceId();
        DeviceInstallation GetDeviceInstallation(params string[] tags);
        DeviceInstallation GetDeviceInstallation();
    }
}
