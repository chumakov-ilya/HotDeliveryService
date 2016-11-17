using Westwind.Utilities.Configuration;

namespace Bringo.HotDeliveryService.Core
{
    public class AppSettings : AppConfiguration, IAppSettings
    {
        public int DeliveryCountMin { get; set; }
        public int DeliveryCountMax { get; set; }
        public int TaskIntervalMin { get; set; }
        public int TaskIntervalMax { get; set; }
        public int ExpirationTime { get; set; }

        public StorageType StorageType { get; set; }
        public string StoragePath { get; set; }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            var provider = new ConfigurationFileConfigurationProvider<AppSettings>()
            {
                EncryptionKey = "appSettings"
            };

            return provider;
        }
    }
}