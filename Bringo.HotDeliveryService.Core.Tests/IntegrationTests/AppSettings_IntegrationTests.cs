using System;
using Bringo.HotDeliveryService.Core.Contracts;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class AppSettings_IntegrationTests
    {
        [Test]
        public void Settings_ReadFromFile_ReturnsAnyValue()
        {
            var actual = Root.Resolve<IAppSettings>();

            int value = (int)actual.StorageType;

            Assert.IsTrue(value > 0);
        }
    }
}