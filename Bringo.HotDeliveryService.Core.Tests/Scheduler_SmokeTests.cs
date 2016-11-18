using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public class Scheduler_SmokeTests
    {
        [Test]
        public void InfiniteRunAsync_RealJobs_Runs()
        {
            var scheduler = DiRoot.Resolve<Scheduler>();
            var j1 = DiRoot.Resolve<CreateJob>();
            var j2 = DiRoot.Resolve<ExpireJob>();
            scheduler.Run(new CancellationToken(false), j1, j2);
        }
    }
}
