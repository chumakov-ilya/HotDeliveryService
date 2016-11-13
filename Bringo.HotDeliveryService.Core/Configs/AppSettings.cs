using Westwind.Utilities.Configuration;

namespace Bringo.HotDeliveryService.Core.Configs
{
    public enum StorageType
    {
        Sqlite = 1,
        Json = 2
    }

    public interface IAppSettings
    {
        int DeliveryCountMax { get; set; }
        int DeliveryCountMin { get; set; }
        int ExpirationTime { get; set; }
        int TaskIntervalMax { get; set; }
        int TaskIntervalMin { get; set; }

        StorageType StorageType { get; set; }

        //string PathToJsonFile { get; set; }

        //IAppSettings ReadFromFile();
    }

    public class AppSettings : AppConfiguration, IAppSettings
    {
        public int DeliveryCountMin { get; set; }
        public int DeliveryCountMax { get; set; }
        public int TaskIntervalMin { get; set; }
        public int TaskIntervalMax { get; set; }
        public int ExpirationTime { get; set; }

        public StorageType StorageType { get; set; }

        public string PathToJsonFile { get; set; }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            var provider = new ConfigurationFileConfigurationProvider<AppSettings>()
            {
                EncryptionKey = "appSettings"
            };

            return provider;
        }

        //public IAppSettings ReadFromFile()
        //{
        //    this.Initialize();
        //    return this;
        //}
    }
}