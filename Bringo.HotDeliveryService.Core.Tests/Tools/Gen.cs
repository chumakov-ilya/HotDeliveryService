using System;

namespace Bringo.HotDeliveryService.Core.Tests
{
    public static class Gen
    {
        private static Random Random { get; set; } = new Random();
        public static int Id { get; set; } = Random.Next();
        public static string Text { get; set; } = Guid.NewGuid().ToString();
    }
}