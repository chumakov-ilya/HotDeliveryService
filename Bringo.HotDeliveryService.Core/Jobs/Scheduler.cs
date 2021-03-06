﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Contracts;

namespace Bringo.HotDeliveryService.Core.Jobs
{
    public class Scheduler
    {
        public IDeliveryPolicy Policy { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public Scheduler(IDeliveryPolicy policy)
        {
            Policy = policy;
        }

        public void Run(CancellationToken cancellationToken, params IJob[] jobs)
        {
            CancellationToken = cancellationToken;

            var tasks = jobs.Select(RunJobAsync).ToArray();

            Task.WaitAll(tasks);
        }

        public async Task RunJobAsync(IJob job)
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