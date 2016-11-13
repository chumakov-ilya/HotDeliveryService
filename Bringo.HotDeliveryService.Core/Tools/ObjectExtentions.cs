using Newtonsoft.Json;

namespace Bringo.HotDeliveryService.Core
{
    public static class ObjectExtentions
    {
        public static string Serialize(this object str)
        {
            return JsonConvert.SerializeObject(str, Formatting.Indented);
        }
    }
}