using System.Threading.Tasks;

namespace Bringo.HotDeliveryService.Core
{
    public interface IJob
    {
        Task RunAsync();
    }
}