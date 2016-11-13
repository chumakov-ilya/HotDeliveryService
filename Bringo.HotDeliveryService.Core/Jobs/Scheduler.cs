using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Configs;

namespace Bringo.HotDeliveryService.Core
{
    public class Scheduler
    {
        public Scheduler(RandomDeliveryPolicy policy)
        {
            Policy = policy;
        }

        public RandomDeliveryPolicy Policy { get; set; }

        public void InfiniteRunAsync(params IJob[] jobs)
        {
            var tasks = jobs.Select(InfiniteRunAsync).ToArray();

            Task.WaitAll(tasks);
        }

        public async Task InfiniteRunAsync(IJob job)
        {
            //TODO: cancellation
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(Policy.GetDelay())).ConfigureAwait(false);

                Trace.WriteLine($"Job {job.GetType().Name} is called");

                await job.RunAsync().ConfigureAwait(false);
            }
        }
    }
}