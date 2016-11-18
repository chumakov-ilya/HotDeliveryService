using System.Diagnostics;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public class CreateJob : IJob
    {
        public RandomDeliveryPolicy Policy { get; set; }

        public DeliveryService Service { get; set; }

        public DeliveryFactory Factory { get; set; }

        public CreateJob(RandomDeliveryPolicy policy, DeliveryService service, DeliveryFactory factory)
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