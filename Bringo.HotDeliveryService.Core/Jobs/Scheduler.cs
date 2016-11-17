using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public class Scheduler
    {
        public RandomDeliveryPolicy Policy { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public Scheduler(RandomDeliveryPolicy policy)
        {
            Policy = policy;
        }

        public void Run(CancellationToken cancellationToken, params IJob[] jobs)
        {
            CancellationToken = cancellationToken;

            var tasks = jobs.Select(InfiniteRunAsync).ToArray();

            Task.WaitAll(tasks);
        }

        public async Task InfiniteRunAsync(IJob job)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(Policy.GetDelay()), CancellationToken).ConfigureAwait(false);

                Trace.WriteLine($"Job {job.GetType().Name} is called");

                await job.RunAsync().ConfigureAwait(false);
            }
        }
    }
}