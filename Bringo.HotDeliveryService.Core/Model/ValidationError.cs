namespace Bringo.HotDeliveryService.Core
{
    public class ValidationError
    {
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }
}