using System;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public static class Gen
    {
        private static Random Random => new Random();

        public static int Id => Random.Next();

        public static string Text => Guid.NewGuid().ToString();
    }
}