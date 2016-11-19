using System.Diagnostics;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;
using Bringo.HotDeliveryService.Core.Services;

namespace Bringo.HotDeliveryService.Core.Jobs
{
    public class CreateJob : IJob
    {
        public IDeliveryPolicy Policy { get; set; }

        public DeliveryService Service { get; set; }

        public DeliveryFactory Factory { get; set; }

        public CreateJob(IDeliveryPolicy policy, DeliveryService service, DeliveryFactory factory)
        {
            Policy = policy;
            Service = service;
            Factory = factory;
        }

        public async Task RunAsync()
        {
            int countToCreate = Policy.GetDeliveryCount();

            var deliveries = Factory.CreateDeliveries(countToCreate);

            Trace.WriteLine($"Creating {countToCreate} deliveries.");

            await Service.InsertAsync(deliveries);
        }

    }
}