namespace Bringo.HotDeliveryService.Core
{
    public interface IAppSettings
    {
        int DeliveryCountMax { get; set; }
        int DeliveryCountMin { get; set; }
        int DeliveryLifetime { get; set; }
        int TaskIntervalMax { get; set; }
        int TaskIntervalMin { get; set; }

        StorageType StorageType { get; set; }

        string GetStoragePath();
    }
}