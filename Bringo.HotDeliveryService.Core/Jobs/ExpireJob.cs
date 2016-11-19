using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public class ExpireJob : IJob
    {
        public DeliveryService Service { get; set; }

        public ExpireJob(DeliveryService service)
        {
            Service = service;
        }

        public async Task RunAsync()
        {
            await Service.MarkAsExpiredAsync();
        }
    }
}