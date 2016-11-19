namespace Bringo.HotDeliveryService.Core
{
    public interface IDeliveryPolicy
    {
        int GetDeliveryCount();

        int GetDelay();
    }
}