using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core.Contracts
{
    public interface IJob
    {
        Task RunAsync();
    }
}