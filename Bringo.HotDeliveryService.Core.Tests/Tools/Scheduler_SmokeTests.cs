using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Jobs;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class Scheduler_SmokeTests
    {
        [Test]
        public void InfiniteRunAsync_RealJobs_Runs()
        {
            var scheduler = Root.Resolve<Scheduler>();
            var j1 = Root.Resolve<CreateJob>();
            var j2 = Root.Resolve<ExpireJob>();
            scheduler.Run(new CancellationToken(false), j1, j2);
        }
    }
}
