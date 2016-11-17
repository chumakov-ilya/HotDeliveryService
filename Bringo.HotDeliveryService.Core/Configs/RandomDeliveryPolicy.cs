using System;

namespace Bringo.HotDeliveryService.Core
{
    public class RandomDeliveryPolicy
    {
        public IAppSettings Settings { get; set; }

        public Random Random { get; set; }

        public RandomDeliveryPolicy(IAppSettings settings)
        {
            Settings = settings;
            Random = new Random();
        }

        public int GetDeliveryCount()
        {
            return Random.Next(Settings.DeliveryCountMin, Settings.DeliveryCountMax);
        }

        public int GetDelay()
        {
            return Random.Next(Settings.TaskIntervalMin, Settings.TaskIntervalMax);
        }
    }
}