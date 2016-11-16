using System;

namespace Bringo.HotDeliveryService.Core
{
    public enum DeliveryStatusEnum
    {
        Available = 1,
        Taken = 2,
        Expired = 3
    }

    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }

        public ValidationError[] ValidationErrors { get; set; }

    }

    public class ValidationError
    {
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }

    public class Filter
    {
        public DeliveryStatusEnum Status { get; set; }
        //public string Status { get; set; }
    }
}