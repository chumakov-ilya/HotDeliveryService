using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Services;

namespace Bringo.HotDeliveryService.Core.Jobs
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