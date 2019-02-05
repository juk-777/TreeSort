using System.Threading;
using System.Threading.Tasks;

namespace TreeSort.BusinessLogic
{
    public interface ITreeBusinessLogic
    {
        Task StartJobAsync(CancellationToken token);
    }
}
