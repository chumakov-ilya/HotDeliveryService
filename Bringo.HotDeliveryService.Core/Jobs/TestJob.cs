using System.Diagnostics;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public class TestJob : IJob
    {
        public async Task RunAsync()
        {
            await Task.Run(() => Trace.WriteLine(nameof(TestJob)));
        }
    }
}