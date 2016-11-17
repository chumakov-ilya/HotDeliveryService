using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public class ExpireJob : IJob
    {
        public TimeSpan ExpirationInterval { get; set; }

        public DeliveryService Service { get; set; }

        public ExpireJob(DeliveryService service, IAppSettings settings)
        {
            Service = service;
            ExpirationInterval = TimeSpan.FromSeconds(settings.ExpirationTime);
        }

        public async Task RunAsync()
        {
            var expirationTime = DateTime.Now - ExpirationInterval;

            await Service.MarkAsExpired(expirationTime);
        }
    }
}