namespace Bringo.HotDeliveryService.Core.Contracts
{
    public interface IDeliveryPolicy
    {
        int GetDeliveryCount();

        int GetDelay();
    }
}