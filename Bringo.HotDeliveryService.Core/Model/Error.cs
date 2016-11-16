namespace Bringo.HotDeliveryService.Core
{
    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorText { get; set; }

        public ValidationError[] ValidationErrors { get; set; }

    }
}