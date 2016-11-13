using Newtonsoft.Json;

namespace Bringo.HotDeliveryService.Core
{
    public static class StringExtentions
    {
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        public static T Deserialize<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}