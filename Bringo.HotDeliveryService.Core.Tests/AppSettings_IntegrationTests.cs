using System;
using Bringo.HotDeliveryService.Core.Configs;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class AppSettings_IntegrationTests
    {
        [Test]
        public void Settings_ReadFromFile_ReturnsAnyValue()
        {
            var actual = DiRoot.Resolve<IAppSettings>();

            int value = (int)actual.StorageType;

            Assert.IsTrue(value > 0);
        }
    }
}