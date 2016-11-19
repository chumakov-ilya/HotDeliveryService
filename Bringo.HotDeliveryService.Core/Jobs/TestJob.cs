using System.Diagnostics;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;

namespace Bringo.HotDeliveryService.Core.Jobs
{
    public class TestJob : IJob
    {
        public async Task RunAsync()
        {
            await Task.Run(() => Trace.WriteLine(nameof(TestJob)));
        }
    }
}