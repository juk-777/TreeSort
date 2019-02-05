using System.Threading;

namespace TreeSort.BusinessLogic
{
    public interface ITreeBusinessLogic
    {
        void StartJob(CancellationToken token);
    }
}
