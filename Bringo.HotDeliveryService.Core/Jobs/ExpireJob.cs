using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Bringo.HotDeliveryService.Core.Configs;

namespace Bringo.HotDeliveryService.Core
{
    public class ExpireJob : IJob
    {
        public TimeSpan ExpirationInterval { get; set; }

        public IRepository Repository { get; set; }

        public ExpireJob(IRepository repository, IAppSettings settings)
        {
            Repository = repository;
            ExpirationInterval = TimeSpan.FromSeconds(settings.ExpirationTime);
        }

        public async Task RunAsync()
        {
            var expirationTime = DateTime.Now - ExpirationInterval;

            await Repository.MarkAsExpired(expirationTime);
        }
    }
}