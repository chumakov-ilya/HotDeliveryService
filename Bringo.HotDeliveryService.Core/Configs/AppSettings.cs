using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Westwind.Utilities.Configuration;

namespace Bringo.HotDeliveryService.Core
{
    public class AppSettings : AppConfiguration, IAppSettings
    {
        public int DeliveryCountMin { get; set; }
        public int DeliveryCountMax { get; set; }
        public int TaskIntervalMin { get; set; }
        public int TaskIntervalMax { get; set; }
        public int DeliveryLifetime { get; set; }

        public StorageType StorageType { get; set; }

        public string GetStoragePath()
        {
            string assemblyDir = AssemblyDirectory;

            //GetParent() is needed to avoid writings to \bin folder (overwise it causes IIS pool recycle).
            string parent = Directory.GetParent(assemblyDir).FullName;

            string storagePath = Path.Combine(parent, "Storage\\");

            return storagePath;
        }

        protected override IConfigurationProvider OnCreateDefaultProvider(string sectionName, object configData)
        {
            var provider = new ConfigurationFileConfigurationProvider<AppSettings>()
            {
                EncryptionKey = "appSettings"
            };

            return provider;
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}